using doremi.DAL;
using doremi.Data;
using doremi.Models;
using doremi.Models.SyncfusionViewModels;
using doremi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NRules;
using NRules.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace doremi.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/SalesOrder")]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;
        private readonly IEmailSender _emailSender;
        private LogHelper LogHelper = new LogHelper();

        public SalesOrderController(ApplicationDbContext context,
                        INumberSequence numberSequence,
                        IEmailSender emailSender)
        {
            _context = context;
            _numberSequence = numberSequence;
            _emailSender = emailSender;
        }

        // GET: api/SalesOrder
        [HttpGet]
        public async Task<IActionResult> GetSalesOrder()
        {
            List<SalesOrder> Items = await _context.SalesOrder.ToListAsync();
            int Count = Items.Count();

            double d1 = MathUtil.TruncateDouble(1.23456789d);

            bool testing = false;
            if (testing)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    int i = db.Product.ToList().Count();

                    EmailUtil.GetInstance().SendEmailToGroup("Sales", "Sun Zong", "hao");
                    int j = 0;
                }
            }
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotShippedYet()
        {
            List<SalesOrder> salesOrders = new List<SalesOrder>();
            try
            {
                List<Shipment> shipments = new List<Shipment>();
                shipments = await _context.Shipment.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in shipments)
                {
                    ids.Add(item.SalesOrderId);
                }

                salesOrders = await _context.SalesOrder
                    .Where(x => !ids.Contains(x.SalesOrderId))
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(salesOrders);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            SalesOrder result = await _context.SalesOrder
                .Where(x => x.SalesOrderId.Equals(id))
                .Include(x => x.SalesOrderLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }

        private void UpdateSalesOrder(int salesOrderId)
        {
            try
            {
                SalesOrder salesOrder = new SalesOrder();
                salesOrder = _context.SalesOrder
                    .Where(x => x.SalesOrderId.Equals(salesOrderId))
                    .FirstOrDefault();

                SalesType salesType = _context.SalesType.Where(st => st.SalesTypeId == salesOrder.SalesTypeId).SingleOrDefault();
                if (salesOrder != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.SalesOrderId.Equals(salesOrderId)).ToList();

                    //update master data by its lines
                    salesOrder.Amount = lines.Sum(x => x.Amount);
                    salesOrder.SubTotal = lines.Sum(x => x.SubTotal);

                    salesOrder.Discount = lines.Sum(x => x.DiscountAmount);
                    salesOrder.Tax = lines.Sum(x => x.TaxAmount);

                    double discountPercentage = 0;

                    if (salesType != null)
                    {
                        discountPercentage = salesType.DiscountPercentage;
                    }
                    else

                        salesOrder.Total = salesOrder.Freight + lines.Sum(x => x.Total) * (1 - discountPercentage);

                    _context.Update(salesOrder);

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<SalesOrder> payload)
        {
            SalesOrder salesOrder = payload.value;
            salesOrder.SalesOrderName = _numberSequence.GetNumberSequence("SO");
            _context.SalesOrder.Add(salesOrder);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrder.SalesOrderId);
            Log(salesOrder.SalesOrderName, ApiAction.INSESRT);
            return Ok(salesOrder);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<SalesOrder> payload)
        {
            SalesOrder salesOrder = payload.value;

            //get current db value to compare if the order status changed
            SalesOrder dbSalesOrder = _context.SalesOrder.AsNoTracking().FirstOrDefault(so => so.SalesOrderId == salesOrder.SalesOrderId);

            LogStatusChange(salesOrder.SalesOrderName, ApiAction.UPDATED, dbSalesOrder.OrderProgressTypeId, salesOrder.OrderProgressTypeId);

            RunRule(salesOrder);

            salesOrder.SumUp();//calculate the total

            _context.SalesOrder.Update(salesOrder);
            _context.SaveChanges();

            if (IsApproved(salesOrder))
            {
                //Normal order, send email to warehouse
                SendEmail("Warehouse", "Incoming shipping request for order " + salesOrder.SalesOrderName, "You have one order ready for packing, please check");
            }

            else if (IsShipping(salesOrder))
            {
                SendEmail("Sales", "Order " + salesOrder.SalesOrderName + " is shipped", "Order has been shipped");
            }

            else if(IsCancelled(salesOrder))
            {
                string message = salesOrder.Remarks ?? "Order has been cancelled";
                SendEmail("Sales", "Order " + salesOrder.SalesOrderName + " is cancelled", message);
            }
            else if (IsClosed(salesOrder))
            {
                SendEmail("Sales", "Order " + salesOrder.SalesOrderName + " is delivered and closed", "Order has been delivered");
            }

            return Ok(salesOrder);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<SalesOrder> payload)
        {
            SalesOrder salesOrder = _context.SalesOrder
                .Where(x => x.SalesOrderId == (int)payload.key)
                .FirstOrDefault();
            _context.SalesOrder.Remove(salesOrder);
            _context.SaveChanges();
            Log(salesOrder.SalesOrderName, ApiAction.REMOVED);
            return Ok(salesOrder);
        }

        private Boolean IsConfirmed(SalesOrder salesOrder)
        {
            return salesOrder.OrderProgressTypeId == OrderProgressStatus.CONFIRMED;
        }

        private Boolean IsApproved(SalesOrder salesOrder)
        {
            return salesOrder.OrderProgressTypeId == OrderProgressStatus.APPROVED;
        }

        private Boolean IsShipping(SalesOrder salesOrder)
        {
            return salesOrder.OrderProgressTypeId == OrderProgressStatus.SHIPPING;
        }

        private Boolean IsClosed(SalesOrder salesOrder)
        {
            return salesOrder.OrderProgressTypeId == OrderProgressStatus.CLOSED;
        }

        private Boolean IsCancelled(SalesOrder salesOrder)
        {
            return salesOrder.OrderProgressTypeId == OrderProgressStatus.CANCELLED;
        }

        private Boolean IsBulkOrder(SalesOrder salesOrder)
        {
            return salesOrder.IsBulkOrder;
        }

        private void RunRule(SalesOrder salesOrder)
        {
            //rule start
            var repository = new RuleRepository();
            repository.Load(x => x.From(Assembly.GetExecutingAssembly()));
            var factory = repository.Compile();
            var session = factory.CreateSession();
            session.Insert(salesOrder);
            session.Fire();
            //rule end
        }

        private void SendEmail(string Group, string subject, string message)
        {
            EmailUtil.GetInstance().SendEmailToGroup(Group, subject, message);
        }

        private void Log(string salesOrderName, string action)
        {
            string currentUser = User.Identity.Name;
            LogHelper.Log(salesOrderName + " has been " + action + " by " + currentUser);
        }

        private void LogStatusChange(string salesOrderName, string action, int fromStatusId, int toStatusId)
        {
            if (fromStatusId == toStatusId)
            {
                return;
            }
            string currentUser = User.Identity.Name;
            string fromStatusString = GetNameByProgressId(fromStatusId);
            string toStatusString = GetNameByProgressId(toStatusId);
            LogHelper.Log(salesOrderName + " has been " + action + " from " + fromStatusString + " to " + toStatusString + " by " + currentUser);
        }

        //private Boolean IsOrderProgressTypeChange(int payloadProgressId, int dbProgressId)
        //{
        //    return payloadProgressId != dbProgressId;
        //}

        private string GetNameByProgressId(int id)
        {
            OrderProgressType orderProgressType = new OrderProgressType();
            orderProgressType = _context.OrderProgressType.SingleOrDefault(x => x.OrderProgressTypeId.Equals(id));

            if (orderProgressType != null && orderProgressType.OrderProgressTypeId != 0)
            {
                return orderProgressType.OrderProgressTypeName;
            }
            return "Unknown Status";
        }
    }
}
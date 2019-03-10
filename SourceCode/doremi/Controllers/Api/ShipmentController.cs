using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using doremi.Data;
using doremi.Models;
using doremi.Models.SyncfusionViewModels;
using doremi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NRules;
using NRules.Fluent;

namespace doremi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Shipment")]
    public class ShipmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public ShipmentController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/Shipment
        [HttpGet]
        public async Task<IActionResult> GetShipment()
        {
            List<Shipment> Items = await _context.Shipment.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotInvoicedYet()
        {
            List<Shipment> shipments = new List<Shipment>();
            try
            {
                List<Invoice> invoices = new List<Invoice>();
                invoices = await _context.Invoice.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in invoices)
                {
                    ids.Add(item.ShipmentId);
                }

                shipments = await _context.Shipment
                    .Where(x => !ids.Contains(x.ShipmentId))
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(shipments);
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Shipment> payload)
        {
            Shipment shipment = payload.value;
            shipment.ShipmentName = _numberSequence.GetNumberSequence("DO");
            _context.Shipment.Add(shipment);
            _context.SaveChanges();
            return Ok(shipment);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Shipment> payload)
        {
            Shipment shipment = payload.value;
            _context.Shipment.Update(shipment);
            _context.SaveChanges();
            return Ok(shipment);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Shipment> payload)
        {
            Shipment shipment = _context.Shipment
                .Where(x => x.ShipmentId == (int)payload.key)
                .FirstOrDefault();
            _context.Shipment.Remove(shipment);
            _context.SaveChanges();
            return Ok(shipment);
        }

        [HttpPost("[action]")]
        public bool Shipped([FromBody] Shipment s)
        {
            Shipment shipment = _context.Shipment
     .Where(x => x.TrackingNumber == s.TrackingNumber)
     .FirstOrDefault();
            shipment.IsFullShipment = true;
            //When the shipment is done,
            //then update the shipment
            _context.Shipment.Update(shipment);

            SalesOrder salesOrder = _context.SalesOrder.Where(s1 => s1.SalesOrderId == shipment.SalesOrderId).Single();
            Customer customer = _context.Customer.Where(cust => cust.CustomerId == salesOrder.CustomerId).Single();

            salesOrder.OrderProgressTypeId = OrderProgressStatus.CLOSED;

            //rule start
            var repository = new RuleRepository();
            repository.Load(x => x.From(Assembly.GetExecutingAssembly()));
            var factory = repository.Compile();
            var session = factory.CreateSession();

            session.Insert(salesOrder);
            session.Fire();
            //rule end

            _context.SaveChanges();
            return true;
        }
    }
}
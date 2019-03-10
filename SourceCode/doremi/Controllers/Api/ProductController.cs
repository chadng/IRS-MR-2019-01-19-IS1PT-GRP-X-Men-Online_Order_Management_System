using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doremi.Data;
using doremi.Models;
using doremi.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doremi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            List<Product> Items = await _context.Product.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Product> payload)
        {
            Product product = payload.value;
            _context.Product.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Product> payload)
        {
            Product product = payload.value;
            _context.Product.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Product> payload)
        {
            Product product = _context.Product
                .Where(x => x.ProductId == (int)payload.key)
                .FirstOrDefault();
            _context.Product.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public bool AutoPO([FromBody]CrudViewModel<Product> payload)
        {
            PurchaseOrder po = new PurchaseOrder();

            List<SalesOrderLine> ListOfSalesOrderLine = _context.SalesOrderLine.Where(sol =>
            sol.SalesOrder.OrderProgressTypeId != OrderProgressStatus.CANCELLED &&
           sol.SalesOrder.OrderDate.AddDays(30) > DateTime.Now
            ).ToList();

            List<SalesOrderLine> lResult = ListOfSalesOrderLine.GroupBy(x => x.ProductId).Select(l => new SalesOrderLine()
            {
                ProductId = l.First().ProductId,
                Quantity = l.First().Quantity
            }).ToList();

            List<Product> list = _context.Product.ToList();
            foreach (Product p in list)
            {
                foreach (SalesOrderLine sol in lResult)
                    if (p.ProductId == sol.ProductId || p.Balance < sol.Quantity)
                    {
                        PurchaseOrderLine pol = new PurchaseOrderLine();
                        pol.ProductId = p.ProductId;
                        pol.Quantity = sol.Quantity;
                        pol.PurchaseOrder = po;
                        po.PurchaseOrderLines.Add(pol);
                        po.OrderDate = DateTime.Now;
                        po.PurchaseOrderName = "#PO-" + DateTime.Now.ToString("yyyyMMddHHmm");
                        po.CurrencyId = 1;
                        _context.PurchaseOrderLine.Add(pol);
                    }
            }

            _context.PurchaseOrder.Add(po);

            if (po.PurchaseOrderLines.Count() != 0)
            {
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
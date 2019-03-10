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
    [Authorize]
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            List<Customer> Items = await _context.Customer.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Customer> payload)
        {
            Customer customer = payload.value;
            if (Iscustomeremailexist(customer.Email))
                return Content("Customer email exists!");
            else {
                _context.Customer.Add(customer);
                _context.SaveChanges();
                return Ok(customer);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Customer> payload)
        {
            Customer customer = payload.value;
            _context.Customer.Update(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Customer> payload)
        {
            Customer customer = _context.Customer
                .Where(x => x.CustomerId == (int)payload.key)
                .FirstOrDefault();
            _context.Customer.Remove(customer);
            _context.SaveChanges();
            return Ok(customer);

        }

        private Boolean Iscustomeremailexist(string email)
        {
            var result = _context.Customer.FirstOrDefault(c => c.Email == email);
            if (result == null)
                return false;
            else
                return true;
        }
    }
}
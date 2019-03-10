using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doremi.Data;
using doremi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doremi.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/OrderProgressType")]
    public class OrderProgressTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderProgressTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesType
        [HttpGet]
        public async Task<IActionResult> GetOrderProgressType()
        {
            List<OrderProgressType> Items = await _context.OrderProgressType.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


      
    }
}
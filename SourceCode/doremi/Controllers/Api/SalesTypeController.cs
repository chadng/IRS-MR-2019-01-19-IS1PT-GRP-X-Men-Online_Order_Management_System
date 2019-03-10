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
    [Route("api/SalesType")]
    public class SalesTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesType
        [HttpGet]
        public async Task<IActionResult> GetSalesType()
        {
            List<SalesType> Items = await _context.SalesType.Where(st=>st.From<DateTime.Now&&st.To>DateTime.Now).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<SalesType> payload)
        {
            SalesType salesType = payload.value;
            _context.SalesType.Add(salesType);
            _context.SaveChanges();
            return Ok(salesType);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<SalesType> payload)
        {
            SalesType salesType = payload.value;
            _context.SalesType.Update(salesType);
            _context.SaveChanges();
            return Ok(salesType);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<SalesType> payload)
        {
            SalesType salesType = _context.SalesType
                .Where(x => x.SalesTypeId == (int)payload.key)
                .FirstOrDefault();
            _context.SalesType.Remove(salesType);
            _context.SaveChanges();
            return Ok(salesType);

        }
    }
}
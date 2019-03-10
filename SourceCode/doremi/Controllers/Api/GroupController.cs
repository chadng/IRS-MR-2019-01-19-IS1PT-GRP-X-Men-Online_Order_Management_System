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
    [Route("api/Group")]
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Group
        [HttpGet]
        public async Task<IActionResult> GetGroup()
        {
            List<Group> Items = await _context.Group.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Group> payload)
        {
            Group group = payload.value;
            _context.Group.Add(group);
            _context.SaveChanges();
            return Ok(group);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Group> payload)
        {
            Group group = payload.value;
            _context.Group.Update(group);
            _context.SaveChanges();
            return Ok(group);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Group> payload)
        {
            Group group = _context.Group
                .Where(x => x.GroupId == (int)payload.key)
                .FirstOrDefault();
            _context.Group.Remove(group);
            _context.SaveChanges();
            return Ok(group);

        }
    }
}
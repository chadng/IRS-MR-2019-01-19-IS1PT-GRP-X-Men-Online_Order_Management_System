using System.Linq;
using doremi.Data;
using doremi.Models;
using doremi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    [Authorize(Roles = Pages.MainMenu.SalesOrder.RoleName)]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public SalesOrderController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            SalesOrder salesOrder = _context.SalesOrder.SingleOrDefault(x => x.SalesOrderId.Equals(id));

            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }
    }
}
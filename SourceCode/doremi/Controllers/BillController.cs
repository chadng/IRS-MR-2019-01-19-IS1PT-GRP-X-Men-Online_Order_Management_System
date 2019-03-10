using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    [Authorize(Roles = Pages.MainMenu.Bill.RoleName)]
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
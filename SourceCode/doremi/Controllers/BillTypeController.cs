using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    [Authorize(Roles = Pages.MainMenu.BillType.RoleName)]
    public class BillTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
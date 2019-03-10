using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    [Authorize(Roles = Pages.MainMenu.UnitOfMeasure.RoleName)]
    public class UnitOfMeasureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
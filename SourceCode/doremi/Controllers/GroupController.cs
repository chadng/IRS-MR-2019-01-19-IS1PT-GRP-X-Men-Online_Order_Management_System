using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    [Authorize(Roles = Pages.MainMenu.Group.RoleName)]
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
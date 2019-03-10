using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    public class NumberSequenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
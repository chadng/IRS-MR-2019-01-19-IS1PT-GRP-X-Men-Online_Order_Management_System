﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doremi.Controllers
{
    [Authorize(Roles = Pages.MainMenu.Shipment.RoleName)]
    public class ShipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doremi.Data;
using doremi.Models;
using doremi.Models.SyncfusionViewModels;
using doremi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doremi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Shipment2")]
    public class Shipment2Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public Shipment2Controller(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

         
        [HttpPost("[action]")]
        public bool Shipped([FromBody] Shipment s) {

            Shipment shipment = _context.Shipment
     .Where(x => x.ShipmentId == s.ShipmentId)
     .FirstOrDefault();
            shipment.IsFullShipment = true;
            _context.Shipment.Update(shipment);
            _context.SaveChanges();
            return true;
        }
    }
}
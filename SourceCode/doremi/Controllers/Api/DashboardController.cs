using doremi.DAL;
using doremi.Data;
using doremi.Models;
using doremi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doremi.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        private class PieChart
        {
            public PieChart(string x, string text, int y)
            {
                this.x = x;
                this.text = text;
                this.y = y;
            }

            public string x { get; set; }
            public string text { get; set; }
            public int y { get; set; }
        }

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dashboard/GetSalesOrderCountByStatus
        [HttpGet("GetSalesOrderCountByStatus")]
        public JsonResult GetSalesOrderCountByStatus()
        {
            //get all salesOrder group by progressTypeId
            var Items = _context.SalesOrder
                .GroupBy(so => so.OrderProgressTypeId)
                .Select(g => new { progressId = g.Key, count = g.Count() });

            //construct pieChart Json and return
            List<PieChart> chart = new List<PieChart>();
            foreach (var item in Items) {
                string progressName = GetNameByProgressId(item.progressId);
                chart.Add(new PieChart(x: progressName, text: progressName, y: item.count));
            }
            
            return Json(chart);
        }

        // GET: api/Dashboard/GetBulkCount
        [HttpGet("GetBulkCount")]
        public JsonResult GetBulkCount()
        {
            //get all salesOrder group by isBulkOrder
            var Items = _context.SalesOrder
                .GroupBy(so => so.IsBulkOrder)
                .Select(g => new {isBulk = g.Key, count = g.Count() });

            //construct pieChart Json and return
            List<PieChart> chart = new List<PieChart>();
            foreach (var item in Items)
            {
                if (item.isBulk) 
                    chart.Add(new PieChart(x: "Bulk Order", text: "Bulk Order", y: item.count));
                else
                    chart.Add(new PieChart(x: "Non-Bulk Order", text: "Non-Bulk Order", y: item.count));
            }

            return Json(chart);
        }

        private string GetNameByProgressId(int id)
        {
            OrderProgressType orderProgressType = new OrderProgressType();
            orderProgressType = _context.OrderProgressType.SingleOrDefault(x => x.OrderProgressTypeId.Equals(id));

            if (orderProgressType != null && orderProgressType.OrderProgressTypeId != 0)
            {
                return orderProgressType.OrderProgressTypeName;
            }
            return "Unknown Status";
        }

        
    }
}
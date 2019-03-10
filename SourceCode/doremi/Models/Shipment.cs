using System;
using System.ComponentModel.DataAnnotations;

namespace doremi.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        [Display(Name = "Shipment Number")]
        public string ShipmentName { get; set; }
        [Display(Name = "Sales Order")]
        public int SalesOrderId { get; set; }
        public DateTimeOffset ShipmentDate { get; set; }
        [Display(Name = "Shipment Type")]
        public int ShipmentTypeId { get; set; }
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }
        [Display(Name = "Is Shipped")]
        public bool IsFullShipment { get; set; } = false;
        [Display(Name = "Tracking Number")]
        public String TrackingNumber { get; set; }
    }
}

using System;
 

namespace DummyLogisticsCompanySystem
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
 
        public string ShipmentName { get; set; }
 
        public int SalesOrderId { get; set; }
        public DateTimeOffset ShipmentDate { get; set; }
 
        public int ShipmentTypeId { get; set; }
 
        public int WarehouseId { get; set; }
 
        public String TrackingNumber { get; set; }

        public bool IsFullShipment { get; set; } = true;
    }
}

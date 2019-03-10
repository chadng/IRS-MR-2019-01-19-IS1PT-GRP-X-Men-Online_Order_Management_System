using doremi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doremi.DAL
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Setting.ConnectionString);
            }
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Bill> Bill { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<BillType> BillType { get; set; }

        public DbSet<Branch> Branch { get; set; }

        public DbSet<CashBank> CashBank { get; set; }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<CustomerType> CustomerType { get; set; }

        public DbSet<GoodsReceivedNote> GoodsReceivedNote { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<InvoiceType> InvoiceType { get; set; }

        public DbSet<NumberSequence> NumberSequence { get; set; }

        public DbSet<PaymentReceive> PaymentReceive { get; set; }

        public DbSet<PaymentType> PaymentType { get; set; }

        public DbSet<PaymentVoucher> PaymentVoucher { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<PurchaseType> PurchaseType { get; set; }

        public DbSet<SalesOrder> SalesOrder { get; set; }

        public DbSet<SalesOrderLine> SalesOrderLine { get; set; }

        public DbSet<SalesType> SalesType { get; set; }
        public DbSet<OrderProgressType> OrderProgressType { get; set; }

        public DbSet<Shipment> Shipment { get; set; }

        public DbSet<ShipmentType> ShipmentType { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<VendorType> VendorType { get; set; }

        public DbSet<Warehouse> Warehouse { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Group> Group { get; set; }
    }
}
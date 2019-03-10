using doremi.DAL;
using doremi.Data;
using doremi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using doremi.DAL;
using doremi.Data;

using doremi.Entities;
using doremi.Models;
using doremi.Models.SyncfusionViewModels;

using doremi.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NRules;
using NRules.Fluent;

namespace doremi.Models
{
    public class SalesOrder
    {
        public int SalesOrderId { get; set; }

        [Display(Name = "Order Number")]
        public string SalesOrderName { get; set; }

        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Display(Name = "Customer Ref. Number")]
        public string CustomerRefNumber { get; set; }

        [Display(Name = "Sales Type")]
        public int SalesTypeId { get; set; }

        public string Remarks { get; set; }
        public double Amount { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public double Freight { get; set; }
        public double Total { get; set; }

        /* Check Constants.cs for details*/
        public int OrderProgressTypeId { get; set; }

        public void SetOrderProgressTypeId(int progress, String ruleName)
        {
            String fromStatus = new MyDbContext().OrderProgressType.Where(t => t.OrderProgressTypeId == this.OrderProgressTypeId).Single().OrderProgressTypeName;
            String toStatus = new MyDbContext().OrderProgressType.Where(t => t.OrderProgressTypeId == progress).Single().OrderProgressTypeName;
            new LogHelper().Log(this.SalesOrderName + " has been updated from " + fromStatus + " to " + toStatus + " by " + ruleName);
            this.OrderProgressTypeId = progress;
        }

        public void DeductBalance()
        {
            using (MyDbContext db = new MyDbContext())
            {
                List<SalesOrderLine> ListOfSOL = db.SalesOrderLine.Where(sol => sol.SalesOrderId == this.SalesOrderId).ToList();
                List<Product> ListOfProduct = db.Product.ToList();

                foreach (Product p in ListOfProduct)
                {
                    foreach (SalesOrderLine sol in ListOfSOL)
                    {
                        if (p.ProductId == sol.ProductId)
                        {
                            p.Balance -= (int)sol.Quantity;
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public void SetOrdeRemarks(string remarks)
        {
            this.Remarks = remarks;
        }

        public Boolean IsBulkOrder { set; get; } //0 for false 1 for true

        public void CheckBalance()
        {
            using (MyDbContext db = new MyDbContext())
            {
                List<SalesOrderLine> ListOfSol = db.SalesOrderLine.Where(sol => sol.SalesOrderId == this.SalesOrderId).ToList();
                List<Product> ListOfProduct = db.Product.ToList();

                bool isSufficient = true;
                foreach (SalesOrderLine sol in ListOfSol)
                {
                    foreach (Product product in ListOfProduct)
                    {
                        if (sol.ProductId == product.ProductId)
                        {
                            if (product.Balance < sol.Quantity)
                            {
                                this.Remarks = product.ProductName + " Balance is not sufficient";
                                this.OrderProgressTypeId = OrderProgressStatus.CANCELLED;//cancelled;
                                isSufficient = false;
                                return;
                            }
                        }
                    }
                }

                if (isSufficient)
                {
                    this.OrderProgressTypeId = OrderProgressStatus.BALANCE_VERIFIED;
                }
            }
        }

        public void SetBestDiscount()
        {
            using (MyDbContext db = new MyDbContext())
            {
                SalesType bestST = db.SalesType.Where(st => st.From < DateTime.Now && DateTime.Now < st.To).OrderByDescending(st => st.DiscountPercentage).First();
                this.SalesTypeId = bestST.SalesTypeId;
                SumUp();
                //this.OrderProgressTypeId = OrderProgressStatus.BEST_DISCOUNT_APPLIED;
            }
        }

        public void AutoTopupCreditPoints()
        {
            using (MyDbContext db = new MyDbContext())
            {
                //7.15-8.15 0.6
                //1.15-2.15 0.6
                //0.5

                bool inOpen = false;

                if (DateTime.Now.Month == 7 && DateTime.Now.Day >= 15)
                {
                    inOpen = true;
                }
                else if (DateTime.Now.Month == 8 && DateTime.Now.Day <= 15)
                {
                    inOpen = true;
                }
                else if (DateTime.Now.Month == 1 && DateTime.Now.Day >= 15)
                {
                    inOpen = true;
                }
                else if (DateTime.Now.Month == 2 && DateTime.Now.Day <= 15)
                {
                    inOpen = true;
                }
                double n = 0d;
                if (inOpen)
                {
                    n = 0.6d;
                }
                else
                {
                    n = 0.5d;
                }

                db.Customer.Where(c => c.CustomerId == this.CustomerId).Single().TopupVoucher((-1) * Math.Log(0.002 * this.Total + 1, n));
                db.SaveChanges();
            }
        }

        public void SumUp()
        {
            List<SalesOrderLine> lines = new List<SalesOrderLine>();
            double discountPercentage = 0;
            using (MyDbContext db = new MyDbContext())
            {
                //get the percentage of so

                SalesType salesType = db.SalesType.Where(st => st.SalesTypeId == this.SalesTypeId).SingleOrDefault();
                if (salesType != null)
                {
                    discountPercentage = salesType.DiscountPercentage;
                }

                lines = db.SalesOrderLine.Where(x => x.SalesOrderId.Equals(this.SalesOrderId)).ToList();
                this.Amount = lines.Sum(x => x.Amount);
                this.SubTotal = lines.Sum(x => x.SubTotal);
                this.Discount = lines.Sum(x => x.DiscountAmount);
                this.Tax = lines.Sum(x => x.TaxAmount);
                this.Total = this.Freight + lines.Sum(x => x.Total) * (1 - discountPercentage);
            }

            //update master data by its lines
        }

        public List<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();
    }
}
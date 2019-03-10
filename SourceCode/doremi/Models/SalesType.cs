using System;
using System.ComponentModel.DataAnnotations;

namespace doremi.Models
{
    public class SalesType
    {
        public int SalesTypeId { get; set; }

        [Required]
        public string SalesTypeName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Dis %")]
        public double DiscountPercentage { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
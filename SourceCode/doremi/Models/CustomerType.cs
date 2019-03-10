using System.ComponentModel.DataAnnotations;

namespace doremi.Models
{
    public class CustomerType
    {
        public int CustomerTypeId { get; set; }
        [Required]
        public string CustomerTypeName { get; set; }
        public string Description { get; set; }
    }
}

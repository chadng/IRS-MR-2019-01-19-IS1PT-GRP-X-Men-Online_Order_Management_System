using System.ComponentModel.DataAnnotations;

namespace doremi.Models
{
    public class OrderProgressType
    {
        public int OrderProgressTypeId { get; set; }
        [Required]
        public string OrderProgressTypeName { get; set; }
        public string Description { get; set; }
    }
}

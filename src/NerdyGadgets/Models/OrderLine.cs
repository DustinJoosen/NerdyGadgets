using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class OrderLine
    {
        [Key]
        [Required]
        [Column("order")]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Key]
        [Column("product")]
        public int ProductNumber { get; set; }

        [ForeignKey("ProductNumber")]
        public Product Product { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

    }
}

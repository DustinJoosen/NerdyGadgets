using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class Order
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("customer")]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [Required]
        [Column("ordered_at")]
        public DateTime OrderedAt { get; set; }

        [Required]
        [Column("expected_delivery_date")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [StringLength(256)]
        [Column("comments")]
        public string Comments { get; set; }

        [Required]
        [Column("delivered")]
        public bool IsDelivered { get; set; }

        [Required]
        [Column("address")]
        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [Column("discount_price")]
        public decimal DiscountPrice { get; set; }

        [Column("discount_percentage")]
        public int DiscountPercentage { get; set; }

        [Column("status")]
        public int Status { get; set; }


        public virtual ICollection<OrderLine> OrderLines { get; set; }

    }
}

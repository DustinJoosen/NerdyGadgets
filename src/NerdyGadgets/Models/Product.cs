using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class Product
    {
        [Key]
        [Column("product_number")]
        public int ProductNumber { get; set; }

        [Required]
        [StringLength(128)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        [Column("description")]
        public string Description { get; set; }

        [Column("category")]
        public string CategoryCode { get; set; }

        [ForeignKey("CategoryCode")]
        public Category Category { get; set; }

        [Required]
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [StringLength(128)]
        [Column("media")]
        public string? Media { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ICollection<ProductSpec> ProductSpecs { get; set; }

    }
}

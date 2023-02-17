using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class ProductSpec
    {
        [Key]
        [Column("product")]
        public int ProductNumber { get; set; }

        [ForeignKey("ProductNumber")]
        public Product Product { get; set; }

        [Key]
        [Required]
        [StringLength(128)]
        [Column("spec_name")]
        public string SpecName { get; set; }

        [Required]
        [StringLength(256)]
        [Column("spec_val")]
        public string SpecVal { get; set; }
    }
}

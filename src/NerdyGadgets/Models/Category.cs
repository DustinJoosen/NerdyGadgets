using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class Category
    {
        [Key]
        [StringLength(3)]
        [Column("code")]
        public string Code { get; set; }

        [StringLength(128)]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [StringLength(128)]
        [Column("image")]
        public string? Image { get; set; }
        public string FullImagePath
        {
            get
            {
                return "/img/notfound.jpg";
            }
        }

        public virtual ICollection<Product>? Products { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class CouponCode
    {
        [Key]
        [StringLength(32)]
        [Column("coupon")]
        public Guid Coupon { get; set; }

        [Required]
        [Column("percentage")]
        public decimal Percentage { get; set; }

        [Required]
        [Column("one_time_use")]
        public bool OneTimeUse { get; set; } = false;
    }
}

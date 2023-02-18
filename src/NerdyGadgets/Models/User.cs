using NerdyGadgets.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class User
    {

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(64)]
        [Column("firstname")]
        public string FirstName { get; set; }

        [StringLength(32)]
        [Column("preposition")]
        public string? Preposition { get; set; }
       
        [Required]
        [StringLength(64)]
        [Column("lastname")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                var preposition = ((String.IsNullOrEmpty(this.Preposition)) ? this.Preposition : "") + " ";
                return $"{this.FirstName} {preposition}{this.LastName}";
            }
        }

        [Required]
        [StringLength(128)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [Column("address")]
        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [Required]
        [Column("account_created_at")]
        public DateTime AccountCreatedAt { get; set; }

        [Required]
        [Column("role")]
        public UserRole Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

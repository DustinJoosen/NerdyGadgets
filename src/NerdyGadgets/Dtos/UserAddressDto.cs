using System.ComponentModel.DataAnnotations;

namespace NerdyGadgets.Dtos
{
    public class UserAddressDto
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Preposition")]
        public string? Preposition { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
    }
}

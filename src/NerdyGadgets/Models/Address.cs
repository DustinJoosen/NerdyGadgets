using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NerdyGadgets.Models
{
    public class Address
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("street")]
        public string? Street { get; set; }

        [Column("zipcode")]
        public string? ZipCode { get; set; }

    }
}

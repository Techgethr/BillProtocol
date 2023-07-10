using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillProtocol.Data
{
    public class UserInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public string? CompanyName { get; set; }

        [Required]
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressState { get; set; }

        [Required]
        public string? AddressCity { get; set; }

        [Required]
        public string? AddressPostalCode { get; set; }

        [Required]
        public int? CountryId { get; set; }

        [Required]
        public string? TaxId { get; set; }

        [Required]
        [ForeignKey("CountryId")]
        public Country? Country { get; set; }
    }
}

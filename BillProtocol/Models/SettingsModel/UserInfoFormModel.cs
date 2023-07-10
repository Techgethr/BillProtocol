using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.SettingsModel
{
    public class UserInfoFormModel
    {
        [Required]
        [DisplayName("First name")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string? LastName { get; set; }

        [DisplayName("Company name")]
        public string? CompanyName { get; set; }

        [Required]
        [DisplayName("Address line 1")]
        public string? AddressLine1 { get; set; }

        [DisplayName("Address line 2")]
        public string? AddressLine2 { get; set; }

        [DisplayName("State")]
        public string? AddressState { get; set; }

        [Required]
        [DisplayName("City")]
        public string? AddressCity { get; set; }

        [Required]
        [DisplayName("Postal code")]
        public string? AddressPostalCode { get; set; }

        [Required]
        [DisplayName("Country")]
        public int? CountryId { get; set; }

        [Required]
        [DisplayName("Tax Id")]
        public string? TaxId { get; set; }
    }
}

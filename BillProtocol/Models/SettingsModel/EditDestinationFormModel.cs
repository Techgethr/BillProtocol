using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.SettingsModel
{
    public class EditDestinationFormModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [DisplayName("Email")]
        public string? Address { get; set; }
    }
}

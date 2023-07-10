using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.SettingsModel
{
    public class EditWalletFormModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}

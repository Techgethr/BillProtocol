using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.SettingsModel
{
    public class AddWalletFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}

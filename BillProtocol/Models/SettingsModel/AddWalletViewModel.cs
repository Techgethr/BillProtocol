using BillProtocol.Data;

namespace BillProtocol.Models.SettingsModel
{
    public class AddWalletViewModel
    {
        public AddWalletFormModel Form { get; set; }

        public AddWalletViewModel(ApplicationDbContext db, string? name)
        {
            
        }
    }
}

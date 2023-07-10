using BillProtocol.Data;

namespace BillProtocol.Models.SettingsModel
{
    public class EditWalletViewModel
    {
        public EditWalletFormModel Form { get; set; }

        public EditWalletViewModel(ApplicationDbContext db, Guid id)
        {
            var wallet = db.Wallets.SingleOrDefault(x => x.Id == id);
            Form = new EditWalletFormModel { Address = wallet.Address, Name = wallet.Name, Id = id };
        }

        



    }
}

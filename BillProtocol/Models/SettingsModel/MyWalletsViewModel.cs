using BillProtocol.Data;

namespace BillProtocol.Models.SettingsModel
{
    public class MyWalletsViewModel
    {
        public IEnumerable<Wallet> Wallets { get; set; }

        public MyWalletsViewModel(ApplicationDbContext db, string? name)
        {
            Wallets = db.Wallets.Where(x => x.UserId == name).OrderBy(x => x.Name);
        }
    }
}

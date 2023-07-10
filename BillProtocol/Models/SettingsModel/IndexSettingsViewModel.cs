using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.SettingsModel
{
    public class IndexSettingsViewModel
    {
        public UserInfo UserInfo { get; set; }
        public int WalletCount { get; set; }
        public int DestinationCount { get; set; }

        public IndexSettingsViewModel(ApplicationDbContext db, string? name)
        {
            UserInfo = db.UserInfos.Include(x => x.Country).SingleOrDefault(x => x.UserId == name);
            WalletCount = db.Wallets.Where(x => x.UserId == name).Count();
            DestinationCount = db.Destinations.Where(x => x.UserId == name).Count();
        }
    }
}

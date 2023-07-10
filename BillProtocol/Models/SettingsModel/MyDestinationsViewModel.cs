using BillProtocol.Data;

namespace BillProtocol.Models.SettingsModel
{
    public class MyDestinationsViewModel
    {
        public IEnumerable<Destination> Destinations { get; set; }

        public MyDestinationsViewModel(ApplicationDbContext db, string? name)
        {
            Destinations = db.Destinations.Where(x => x.UserId == name).OrderBy(x => x.Name);
        }
    }
}

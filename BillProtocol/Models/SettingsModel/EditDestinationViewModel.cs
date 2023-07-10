using BillProtocol.Data;

namespace BillProtocol.Models.SettingsModel
{
    public class EditDestinationViewModel
    {
        public EditDestinationFormModel Form { get; set; }

        public EditDestinationViewModel(ApplicationDbContext db, Guid id)
        {
            var wallet = db.Destinations.SingleOrDefault(x => x.Id == id);
            Form = new EditDestinationFormModel { Address = wallet.Address, Name = wallet.Name, Id = id };
        }
    }
}

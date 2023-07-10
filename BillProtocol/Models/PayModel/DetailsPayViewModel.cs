using BillProtocol.Data;

namespace BillProtocol.Models.PayModel
{
    public class DetailsPayViewModel
    {
        public Invoice Invoice { get; set; }

        public DetailsPayViewModel(ApplicationDbContext db, Guid id)
        {
            Invoice = db.Invoices.SingleOrDefault(x => x.Id == id);
        }
    }
}

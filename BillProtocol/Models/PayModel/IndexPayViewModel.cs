using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.PayModel
{
    public class IndexPayViewModel
    {
        public IEnumerable<Invoice> Invoices { get;set; }

        public IndexPayViewModel(ApplicationDbContext db, string? name)
        {
            Invoices = db.Invoices.Where(x => x.Destination.Address == name)
                .Include(x => x.Wallet).Include(x => x.Destination).Include(x => x.Currency)
                .Include(x => x.InvoiceStatus).Include(x => x.InvoiceType)
                .OrderByDescending(x => x.CreatedAt);
        }
    }
}

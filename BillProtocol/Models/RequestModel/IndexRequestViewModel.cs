using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.RequestModel
{
    public class IndexRequestViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }

        public IndexRequestViewModel(ApplicationDbContext db, string? userName)
        {
            Invoices = db.Invoices.Include(x => x.Wallet).Include(x => x.Destination)
                .Include(x => x.InvoiceStatus).Include(x => x.InvoiceType).Where(x => x.UserId == userName).OrderByDescending(x => x.CreatedAt);
        }
    }
}

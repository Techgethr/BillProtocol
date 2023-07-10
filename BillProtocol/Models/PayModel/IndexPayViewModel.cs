using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.PayModel
{
    public class IndexPayViewModel
    {
        public IEnumerable<Invoice> PendingInvoices { get;set; }

        public IndexPayViewModel(ApplicationDbContext db, string? name)
        {
            PendingInvoices = db.Invoices.Where(x => x.Destination.Address == name && x.InvoiceStatusId == Constants.CreatedInvoiceStatus)
                .OrderByDescending(x => x.CreatedAt);
        }
    }
}

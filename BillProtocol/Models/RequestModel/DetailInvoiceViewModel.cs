using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.RequestModel
{
    public class DetailInvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<InvoiceDetail> Items { get; set; }

        public DetailInvoiceViewModel(ApplicationDbContext db, Guid id)
        {
            Invoice = db.Invoices.Include(x => x.Wallet).Include(x => x.Currency)
                .Include(x => x.InvoiceType).Include(x => x.InvoiceStatus).Include(x => x.Destination).SingleOrDefault(x => x.Id == id);
            Items = db.InvoiceDetails.Where(x => x.InvoiceId == id);
        }
    }
}

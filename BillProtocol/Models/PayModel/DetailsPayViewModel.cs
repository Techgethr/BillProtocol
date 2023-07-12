using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.PayModel
{
    public class DetailsPayViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<InvoiceDetail> Items { get; set; }
        public long UnixDateTimeFinish { get;set; }

        public DetailsPayViewModel(ApplicationDbContext db, Guid id)
        {
            Invoice = db.Invoices.Include(x => x.Wallet).Include(x => x.Currency)
                .Include(x => x.InvoiceType).Include(x => x.InvoiceStatus).SingleOrDefault(x => x.Id == id);
            Items = db.InvoiceDetails.Where(x => x.InvoiceId == id);
            UnixDateTimeFinish = (long)(Invoice.PaymentDate - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}

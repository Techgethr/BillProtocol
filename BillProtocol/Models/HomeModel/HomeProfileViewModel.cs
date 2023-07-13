using BillProtocol.Data;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.HomeModel
{
    public class HomeProfileViewModel
    {
        public int PendingInvoices { get; set; }
        public int PendingReceivedInvoices { get; set; }
        public int PayedInvoices { get; set; }
        public int WalletsQty { get; set; }
        public int DestinationsQty { get; set; }

        public HomeProfileViewModel(ApplicationDbContext db, string? name)
        {
            WalletsQty = db.Wallets.Count(x => x.UserId == name);
            DestinationsQty = db.Destinations.Count(x => x.UserId == name);
            PendingReceivedInvoices = db.Invoices.Include(x => x.Destination).Count(x => x.Destination.Address == name && (x.InvoiceStatusId == Constants.CreatedInvoiceStatus || x.InvoiceStatusId == Constants.ApprovedInvoiceStatus));
            PendingInvoices = db.Invoices.Count(x => x.UserId == name && (x.InvoiceStatusId == Constants.CreatedInvoiceStatus || x.InvoiceStatusId == Constants.ApprovedInvoiceStatus));
            PayedInvoices = db.Invoices.Count(x => x.UserId == name && x.InvoiceStatusId == Constants.PayedInvoiceStatus);
        }
    }
}

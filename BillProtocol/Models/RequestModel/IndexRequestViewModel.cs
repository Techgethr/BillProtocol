using BillProtocol.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.RequestModel
{
    public class IndexRequestViewModel
    {
        public IndexRequestFormModel Form { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<SelectListItem> Status { get;set; }
        public IEnumerable<SelectListItem> Wallets { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public IndexRequestViewModel(ApplicationDbContext db, string? userName)
        {
            Form = new IndexRequestFormModel();
            Status = db.InvoiceStatus.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            Wallets = db.Wallets.Where(x => x.UserId == userName).OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            Currencies = db.Currencies.Where(x => x.Enabled == true).OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            Types = db.InvoiceTypes.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
        }
    }
}

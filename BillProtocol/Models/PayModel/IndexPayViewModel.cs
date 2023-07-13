using BillProtocol.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Models.PayModel
{
    public class IndexPayViewModel
    {
        public IEnumerable<Invoice> Invoices { get;set; }
        public IndexPayFormModel Form { get;set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public IndexPayViewModel(ApplicationDbContext db, string? name)
        {
            Form = new IndexPayFormModel();
            Status = db.InvoiceStatus.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            Currencies = db.Currencies.Where(x => x.Enabled == true).OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            Types = db.InvoiceTypes.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
        }
    }
}

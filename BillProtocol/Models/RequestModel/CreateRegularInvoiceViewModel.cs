using BillProtocol.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BillProtocol.Models.RequestModel
{
    public class CreateRegularInvoiceViewModel
    {
        private ApplicationDbContext db;
        public CreateRegularInvoiceFormModel Form { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
        public IEnumerable<SelectListItem> Wallets { get; set; }
        public IEnumerable<SelectListItem> Destinations { get; set; }
        public UserInfo UserInfo { get; set; }
        public int InvoiceNumber { get; set; }

        public CreateRegularInvoiceViewModel(ApplicationDbContext db, string? userName)
        {
            this.db = db;
            UserInfo = db.UserInfos.SingleOrDefault(x => x.UserId == userName);
            InvoiceNumber = db.Invoices.Count(x => x.UserId == userName) + 1;
            Currencies = db.Currencies.Where(x => x.Enabled).OrderBy(x => x.Name)
                .Select(x => new SelectListItem { Text = (x.Name + "(" + x.Code + ")"), Value = x.Id.ToString() });
            Wallets = db.Wallets.Where(x => x.UserId == userName).OrderBy(x => x.Name)
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            Destinations = db.Destinations.Where(x => x.UserId == userName).OrderBy(x => x.Name)
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
        }
    }
}

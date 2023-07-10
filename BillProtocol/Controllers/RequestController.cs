using BillProtocol.Data;
using BillProtocol.Models;
using BillProtocol.Models.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillProtocol.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private ApplicationDbContext _db;

        public RequestController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IndexRequestViewModel model = new IndexRequestViewModel(_db, User.Identity.Name);
            
            return View(model);
        }

        public IActionResult CreateSelector()
        {
            return View();
        }

        public IActionResult CreateRegularInvoice()
        {
            CreateRegularInvoiceViewModel model = new CreateRegularInvoiceViewModel(_db, User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateRegularInvoice(CreateRegularInvoiceFormModel Form)
        {
            if(Form.PaymentDate.HasValue)
            {
                if(Form.PaymentDate.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("Form.PaymentDate", "The payment date must be today or later.");
                }
            }
            if(!ModelState.IsValid)
            {
                CreateRegularInvoiceViewModel model = new CreateRegularInvoiceViewModel(_db, User.Identity.Name);
                model.Form = Form;
                return View(model);
            }

            var invoiceNumer = _db.Invoices.Count(x => x.UserId == User.Identity.Name) + 1;
            Invoice invoice = new Invoice()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UserId = User.Identity.Name,
                InvoiceNumber = invoiceNumer,
                InvoiceStatusId = Constants.CreatedInvoiceStatus,
                InvoiceTypeId = Constants.RegularInvoice,
                DestinationId = Form.DestinationId.Value,
                WalletId = Form.WalletId.Value,
                PaymentDate = Form.PaymentDate.Value,
                CurrencyId = Form.CurrencyId.Value,
                TotalAmount = 0
            }; 
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = invoice.Id });
        }

        public IActionResult CreatePaymentDateInvoice()
        {
            return View();
        }
    }
}

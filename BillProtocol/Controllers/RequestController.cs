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

        public IActionResult CreateInvoice()
        {
            CreateInvoiceViewModel model = new CreateInvoiceViewModel(_db, User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateInvoice(CreateInvoiceFormModel Form)
        {
            if(Form.InvoiceTypeId.HasValue && Form.CurrencyId.HasValue)
            {
                if(Form.InvoiceTypeId == Constants.EscrowInvoice && Form.CurrencyId != Constants.XRPCurrency)
                {
                    ModelState.AddModelError("Form.CurrencyId", "Escrow only allows XRP as a currency.");
                }
            }
            if(Form.Details.Count() == 0)
            {
                ModelState.AddModelError("Form.Details", "You must enter at least one description and its corresponding amount.");
            }
            if(Form.PaymentDate.HasValue)
            {
                if(Form.PaymentDate.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("Form.PaymentDate", "The payment date must be today or later.");
                }
            }
            if(!ModelState.IsValid)
            {
                CreateInvoiceViewModel model = new CreateInvoiceViewModel(_db, User.Identity.Name);
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
                InvoiceTypeId = Form.InvoiceTypeId.Value,
                DestinationId = Form.DestinationId.Value,
                WalletId = Form.WalletId.Value,
                PaymentDate = Form.PaymentDate.Value,
                CurrencyId = Form.CurrencyId.Value,
                TotalAmount = 0, Memo = Form.Memo
            }; 
            
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = invoice.Id });
        }


    }
}

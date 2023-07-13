using BillProtocol.Data;
using BillProtocol.Models;
using BillProtocol.Models.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index(IndexRequestFormModel Form)
        {
            IndexRequestViewModel model = new IndexRequestViewModel(_db, User.Identity.Name);
            IEnumerable<Invoice> invoices = _db.Invoices.Include(x => x.Wallet).Include(x => x.Destination).Include(x => x.Currency)
                .Include(x => x.InvoiceStatus).Include(x => x.InvoiceType).Where(x => x.UserId == User.Identity.Name).OrderByDescending(x => x.CreatedAt);
            if(Form.startDate.HasValue)
            {
                invoices = invoices.Where(x => x.CreatedAt >= Form.startDate.Value.Date);
            }
            if (Form.endDate.HasValue)
            {
                invoices = invoices.Where(x => x.CreatedAt <= Form.endDate.Value.Date);
            }
            if(Form.invoiceType.HasValue)
            {
                invoices = invoices.Where(x => x.InvoiceTypeId == Form.invoiceType.Value);
            }
            if (Form.currency.HasValue)
            {
                invoices = invoices.Where(x => x.CurrencyId == Form.currency.Value);
            }
            if (Form.wallet.HasValue)
            {
                invoices = invoices.Where(x => x.WalletId == Form.wallet.Value);
            }
            if (Form.status.HasValue)
            {
                invoices = invoices.Where(x => x.InvoiceStatusId == Form.status.Value);
            }
            model.Invoices = invoices;
            model.Form = Form;
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
                if (Form.InvoiceTypeId == Constants.PaymentToDateInvoice && Form.CurrencyId != Constants.XRPCurrency)
                {
                    ModelState.AddModelError("Form.CurrencyId", "Payment To Date invoice only allows XRP as a currency.");
                }
            }
            //TODO: Mejor validación
            if(Form.Details != null)
            {
                if(!Form.Details.Any(x => x.Description != null))
                {
                    ModelState.AddModelError("Form.Details", "You must enter at least 1 item.");
                }
                if (Form.Details.Any(x => x.UnitPrice != null && x.Description == null))
                {
                    ModelState.AddModelError("Form.Details", "You must enter at least 1 item.");
                }
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
            Form.Details = Form.Details.Where(x => x.UnitPrice.HasValue && x.Quantity.HasValue && x.Description != null);
            var invoiceNumer = _db.Invoices.Count(x => x.UserId == User.Identity.Name) + 1;
            var invoiceId = Guid.NewGuid();
            Invoice invoice = new Invoice()
            {
                Id = invoiceId,
                CreatedAt = DateTime.UtcNow,
                UserId = User.Identity.Name,
                InvoiceNumber = invoiceNumer,
                InvoiceStatusId = Constants.CreatedInvoiceStatus,
                InvoiceTypeId = Form.InvoiceTypeId.Value,
                DestinationId = Form.DestinationId.Value,
                WalletId = Form.WalletId.Value,
                PaymentDate = Form.PaymentDate.Value,
                CurrencyId = Form.CurrencyId.Value,
                Memo = Form.Memo
            };
            decimal totalInvoiceAmount = 0;
            foreach (var detail in Form.Details)
            {
                //TODO
                decimal totalAmountWithoutTax = ((detail.Quantity.Value * detail.UnitPrice.Value) - detail.Discount.GetValueOrDefault());
                decimal totalAmountWithTax = totalAmountWithoutTax + (detail.Tax.HasValue? (totalAmountWithoutTax * detail.Tax.GetValueOrDefault() / 100) : 0);
                InvoiceDetail invoiceDetail = new InvoiceDetail
                {
                    Description = detail.Description, Discount = detail.Discount, Id = Guid.NewGuid(),
                    InvoiceId = invoiceId, Quantity = detail.Quantity.Value, Tax = detail.Tax, UnitPrice = detail.UnitPrice,
                    TotalAmount = totalAmountWithTax
                };
                
                _db.InvoiceDetails.Add(invoiceDetail);

                totalInvoiceAmount += totalAmountWithTax;
            }
            invoice.TotalAmount = totalInvoiceAmount;
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = invoice.Id });
        }

        public IActionResult Detail (Guid id)
        {
            DetailInvoiceViewModel model = new DetailInvoiceViewModel(_db, id);
            return View(model);
        }

        [HttpPost]
        public IActionResult CashInvoice(Guid idInvoice)
        {
            Invoice invoice = _db.Invoices.SingleOrDefault(x => x.Id == idInvoice);
            invoice.InvoiceStatusId = Constants.PayedInvoiceStatus;
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = idInvoice });
        }

    }
}

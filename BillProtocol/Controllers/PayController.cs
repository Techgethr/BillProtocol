using BillProtocol.Data;
using BillProtocol.Models;
using BillProtocol.Models.PayModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillProtocol.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private ApplicationDbContext _db;

        public PayController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(IndexPayFormModel Form)
        {
            IndexPayViewModel model = new IndexPayViewModel(_db, User.Identity.Name);

            IEnumerable<Invoice> invoices = _db.Invoices
                .Include(x => x.Wallet).Include(x => x.Destination).Include(x => x.Currency)
                .Include(x => x.InvoiceStatus).Include(x => x.InvoiceType)
                .Where(x => x.Destination.Address == User.Identity.Name)
                .OrderByDescending(x => x.CreatedAt);

            if (Form.startDate.HasValue)
            {
                invoices = invoices.Where(x => x.CreatedAt >= Form.startDate.Value.Date);
            }
            if (Form.endDate.HasValue)
            {
                invoices = invoices.Where(x => x.CreatedAt <= Form.endDate.Value.Date);
            }
            if (Form.invoiceType.HasValue)
            {
                invoices = invoices.Where(x => x.InvoiceTypeId == Form.invoiceType.Value);
            }
            if (Form.currency.HasValue)
            {
                invoices = invoices.Where(x => x.CurrencyId == Form.currency.Value);
            }
            if (Form.status.HasValue)
            {
                invoices = invoices.Where(x => x.InvoiceStatusId == Form.status.Value);
            }
            model.Invoices = invoices;
            model.Form = Form;
            return View(model);
        }

        public IActionResult Detail(Guid id)
        {
            DetailsPayViewModel model = new DetailsPayViewModel(_db, id);
            return View(model);
        }

        [HttpPost]
        public IActionResult ApproveReject(Guid idInvoice, string approveButton, string rejectButton, string comments)
        {
            Invoice invoice = _db.Invoices.SingleOrDefault(x => x.Id == idInvoice);
            if(approveButton == "Approve")
            {
                invoice.InvoiceStatusId = Constants.ApprovedInvoiceStatus;
            }
            if (rejectButton == "Reject")
            {
                invoice.InvoiceStatusId = Constants.RejectedInvoiceStatus;
            }
            invoice.InvoiceStatusComments = comments;
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = idInvoice });
        }

        [HttpPost]
        public IActionResult ApproveRejectPaymentInvoice(Guid idInvoice, string approveButton, string rejectButton, string comments, string checkId, long? ledgerSequence)
        {
            if(checkId == null && approveButton == "Approve")
            {
                return RedirectToAction("Detail", new { id = idInvoice });
            }
            Invoice invoice = _db.Invoices.SingleOrDefault(x => x.Id == idInvoice);
            if (checkId != "Approve")
            {
                invoice.InvoiceStatusId = Constants.ApprovedInvoiceStatus;
                invoice.CheckId = checkId;
                invoice.LedgerSequence = ledgerSequence;
            }
            if (rejectButton == "Reject")
            {
                invoice.InvoiceStatusId = Constants.RejectedInvoiceStatus;
            }
            invoice.InvoiceStatusComments = comments;
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = idInvoice });
        }


        [HttpPost]
        public IActionResult PayRegularInvoice(Guid idInvoice)
        {
            Invoice invoice = _db.Invoices.SingleOrDefault(x => x.Id == idInvoice);
            invoice.InvoiceStatusId = Constants.PayedInvoiceStatus;
            _db.SaveChanges();
            return RedirectToAction("Detail", new { id = idInvoice });
        }

        //[HttpPost]
        //public IActionResult RedeemInvoice(Guid idInvoice)
        //{
        //    Invoice invoice = _db.Invoices.SingleOrDefault(x => x.Id == idInvoice);
        //    invoice.InvoiceStatusId = Constants.PayedInvoiceStatus;
        //    _db.SaveChanges();
        //    return RedirectToAction("Detail", new { id = idInvoice });
        //}
    }
}

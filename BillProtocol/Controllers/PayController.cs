using BillProtocol.Data;
using BillProtocol.Models;
using BillProtocol.Models.PayModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            IndexPayViewModel model = new IndexPayViewModel(_db, User.Identity.Name);
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
        public IActionResult Payed(Guid id)
        {
            Invoice invoice = _db.Invoices.SingleOrDefault(x => x.Id == id);
            invoice.InvoiceStatusId = Constants.PayedInvoiceStatus;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = id });
        }
    }
}

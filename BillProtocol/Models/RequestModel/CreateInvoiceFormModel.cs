using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.RequestModel
{
    public class CreateInvoiceFormModel
    {
        [Required]
        [DisplayName("Destination/client")]
        public Guid? DestinationId { get; set; }

        [Required]
        [DisplayName("Wallet")]
        public Guid? WalletId { get; set; }

        [Required]
        [DisplayName("Payment Date")]
        public DateTime? PaymentDate { get; set; }

        [Required]
        [DisplayName("Currency")]
        public int? CurrencyId { get; set; }

        [Required]
        [DisplayName("Invoice type")]
        public int? InvoiceTypeId { get; set; }

        public string? Memo { get;set; }

        public IEnumerable<CreateInvoiceDetailFormModel> Details { get; set; }

        public CreateInvoiceFormModel()
        {
            Details = new List<CreateInvoiceDetailFormModel>();
        }
    }
}

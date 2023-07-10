using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.RequestModel
{
    public class CreateRegularInvoiceFormModel
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
    }
}

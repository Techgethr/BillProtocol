using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillProtocol.Data
{
    public class Invoice
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid WalletId { get; set; }

        [Required]
        public Guid DestinationId { get; set; }

        [Required]
        public int? CurrencyId { get; set; }

        [Required]
        public int? InvoiceNumber { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string? Memo { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public int? InvoiceTypeId { get; set; }

        [Required]
        public long? InvoiceStatusId { get; set; }

        public string? InvoiceStatusComments { get; set; }

        [Required]
        public decimal? TotalAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey("WalletId")]
        public Wallet? Wallet { get; set; }

        [Required]
        [ForeignKey("DestinationId")]
        public Destination? Destination { get; set; }

        [Required]
        [ForeignKey("CurrencyId")]
        public Currency? Currency { get; set; }

        [Required]
        [ForeignKey("InvoiceTypeId")]
        public InvoiceType? InvoiceType { get; set; }

        [Required]
        [ForeignKey("InvoiceStatusId")]
        public InvoiceStatus? InvoiceStatus { get; set; }


    }
}

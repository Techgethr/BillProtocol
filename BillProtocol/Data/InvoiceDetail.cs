using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillProtocol.Data
{
    public class InvoiceDetail
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid InvoiceId { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Tax { get; set; }

        [Required]
        public decimal? TotalAmount { get; set; }

        [Required]
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }
    }
}

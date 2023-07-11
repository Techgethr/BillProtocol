using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Models.RequestModel
{
    public class CreateInvoiceDetailFormModel
    {
        //[Required]
        public string? Description { get; set; }

       // [Required]
        public int? Quantity { get; set; }

        //[Required]
        public decimal? UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Tax { get; set; }
    }
}

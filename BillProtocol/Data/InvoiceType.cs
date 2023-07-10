using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Data
{
    public class InvoiceType
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}

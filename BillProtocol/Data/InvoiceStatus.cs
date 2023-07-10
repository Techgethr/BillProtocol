using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillProtocol.Data
{
    public class InvoiceStatus
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}

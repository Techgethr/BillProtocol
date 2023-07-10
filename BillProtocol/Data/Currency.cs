using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Data
{
    public class Currency
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}

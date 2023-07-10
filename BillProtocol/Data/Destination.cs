using System.ComponentModel.DataAnnotations;

namespace BillProtocol.Data
{
    public class Destination
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string? UserId { get; set; }
    }
}

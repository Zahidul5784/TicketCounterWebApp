using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketCounterWebApp.Models
{
    public class TicketInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InfoId { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        public int? InvoiceId { get; set; }

        public Invoice? Invoice { get; set; }
    }
}

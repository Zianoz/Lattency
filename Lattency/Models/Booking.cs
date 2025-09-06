using System.ComponentModel.DataAnnotations.Schema;

namespace Lattency.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public int NumGuests { get; set; }
        public string Status { get; set; }

        [ForeignKey("Person")]
        public int FK_PersonId { get; set; }
        public Person Person { get; set; }

        [ForeignKey("CafeTable")]
        public int FK_CafeTableId { get; set; }
        public CafeTable CafeTable { get; set; }

    }
}

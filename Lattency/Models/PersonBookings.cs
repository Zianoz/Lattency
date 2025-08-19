using System.ComponentModel.DataAnnotations.Schema;

namespace Lattency.Models
{
    public class PersonBookings
    {
        public int Id { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public int NumGuests { get; set; }

        [ForeignKey("Person")]
        public int FK_PersonId { get; set; }
        public Person Person { get; set; }

        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        public Table Table { get; set; }

    }
}

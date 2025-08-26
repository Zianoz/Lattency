namespace Lattency.Models
{
    public class CafeTable
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
        public string BildURL { get; set; }

        public ICollection<PersonBookings> Bookings { get; set; }
    }
}

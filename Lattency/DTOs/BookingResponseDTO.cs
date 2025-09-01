namespace Lattency.DTOs
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int CafeTableId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public int NumGuests { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
    }
}

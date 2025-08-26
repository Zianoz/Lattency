namespace Lattency.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public ICollection<PersonBookings> Bookings { get; set; }

    }
}

namespace Lattency.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public string? Number { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; }

        //Exclude PasswordHash and Role for security reasons
    }
}

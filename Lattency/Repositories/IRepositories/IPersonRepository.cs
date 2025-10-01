using Lattency.DTOs;
using Lattency.Models;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<Person> GetPersonByIdAsync(int id);
    Task<Person> GetPersonByEmailAsync(string email);
    Task AddPersonAsync(Person person);
    Task UpdatePersonAsync(Person person);
    Task DeletePersonAsync(Person person);
}
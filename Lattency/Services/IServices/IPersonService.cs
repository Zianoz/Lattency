using Lattency.DTOs;
using Lattency.Models;

namespace Lattency.Services.IServices
{
    public interface IPersonService
    {
        //Sends request to PersonService
        Task<string> LoginAsync(LoginDTO dto);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> CreatePersonAsync(PersonDTO dto);
        Task<Person> UpdatePersonAsync(int id, PersonDTO dto);
        Task<bool> DeletePersonAsync(int id);
    }
}

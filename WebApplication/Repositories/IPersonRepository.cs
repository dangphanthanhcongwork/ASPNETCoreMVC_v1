using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> GetPerson(Guid id);
        Task PutPerson(Guid id, Person person);
        Task PostPerson(Person person);
        Task DeletePerson(Guid id);
        Task<bool> PersonExists(Guid id);
        Task<IEnumerable<Person>> GetMales();
        Task<IEnumerable<Person>> GetOldest();
        Task<IEnumerable<Person>> GetByBirthYear(int year);
        Task<IEnumerable<Person>> GetByBirthYearGreaterThan(int year);
        Task<IEnumerable<Person>> GetByBirthYearLessThan(int year);
    }
}
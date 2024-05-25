using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _repository.GetPersons();
        }

        public async Task<Person> GetPerson(Guid id)
        {
            try
            {
                return await _repository.GetPerson(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutPerson(Guid id, Person person)
        {
            try
            {
                await _repository.PutPerson(id, person);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostPerson(Person person)
        {
            await _repository.PostPerson(person);
        }

        public async Task DeletePerson(Guid id)
        {
            try
            {
                await _repository.DeletePerson(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Person>> GetMales()
        {
            return _repository.GetMales();
        }

        public Task<IEnumerable<Person>> GetOldest()
        {
            return _repository.GetOldest();
        }

        public Task<IEnumerable<Person>> GetByBirthYear(int year)
        {
            return _repository.GetByBirthYear(year);
        }

        public Task<IEnumerable<Person>> GetByBirthYearGreaterThan(int year)
        {
            return _repository.GetByBirthYearGreaterThan(year);
        }

        public Task<IEnumerable<Person>> GetByBirthYearLessThan(int year)
        {
            return _repository.GetByBirthYearLessThan(year);
        }
    }
}
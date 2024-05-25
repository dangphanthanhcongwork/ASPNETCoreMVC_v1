using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetPerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id) ?? throw new Exception("Not found!!!");
            return person;
        }

        public async Task PutPerson(Guid id, Person person)
        {
            _context.Persons.Entry(person).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PersonExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PersonExists(Guid id)
        {
            return await _context.Persons.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Person>> GetMales()
        {
            return await _context.Persons.Where(p => p.Gender == Gender.Male).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetOldest()
        {
            return [await _context.Persons.OrderBy(p => p.DateOfBirth).FirstOrDefaultAsync()];
        }

        public async Task<IEnumerable<Person>> GetByBirthYear(int year)
        {
            return await _context.Persons.Where(p => p.DateOfBirth.Year == year).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByBirthYearGreaterThan(int year)
        {
            return await _context.Persons.Where(p => p.DateOfBirth.Year > year).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByBirthYearLessThan(int year)
        {
            return await _context.Persons.Where(p => p.DateOfBirth.Year < year).ToListAsync();
        }
    }
}
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class PersonRepository
    {
        private readonly List<Person> _persons = [
            // Add your dummy data here
            new Person { Id = Guid.NewGuid(), FirstName = "Công", LastName = "Đặng Phan Thành", Gender = Gender.Male, DateOfBirth = new DateTime(2000, 6, 15), PhoneNumber = "0375284637", BirthPlace = "Lâm Đồng", IsGraduated = true },
            new Person { Id = Guid.NewGuid(), FirstName = "Linh", LastName = "Nguyễn Mỹ", Gender = Gender.Female, DateOfBirth = new DateTime(1995, 7, 4), PhoneNumber = "0375284636", BirthPlace = "Hà Nội", IsGraduated = true },
            new Person { Id = Guid.NewGuid(), FirstName = "Phương", LastName = "Nguyễn Thị Mai", Gender = Gender.Female, DateOfBirth = new DateTime(2002, 4, 7), PhoneNumber = "0375284635", BirthPlace = "Hải Phòng", IsGraduated = false },
            new Person { Id = Guid.NewGuid(), FirstName = "Thu", LastName = "Phan Thị Hà", Gender = Gender.Female, DateOfBirth = new DateTime(2003, 2, 27), PhoneNumber = "0375284634", BirthPlace = "Huế", IsGraduated = false },
            new Person { Id = Guid.NewGuid(), FirstName = "Quang", LastName = "Trần Huy", Gender = Gender.Male, DateOfBirth = new DateTime(1994, 4, 20), PhoneNumber = "0375284633", BirthPlace = "Hà Nội", IsGraduated = false },
        ];

        // ADD
        public async Task Add(Person person)
        {
            _persons.Add(person);
            await Task.CompletedTask;
        }

        // GET
        public async Task<IEnumerable<Person>> Get()
        {
            return await Task.FromResult(_persons.AsEnumerable());
        }

        public async Task<Person> Get(Guid id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id) ?? throw new Exception($"Person {id} not found!!!");
            return await Task.FromResult(person);
        }

        // EDIT
        public async Task Update(Person person)
        {
            var index = _persons.FindIndex(p => p.Id == person.Id);
            if (index != -1)
            {
                _persons[index] = person;
            }
            await Task.CompletedTask;
        }

        // DELETE
        public async Task Delete(Guid id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                _persons.Remove(person);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Person>> GetMales()
        {
            return await Task.FromResult(_persons.Where(p => p.Gender == Gender.Male).ToList().AsEnumerable());
        }

        public async Task<IEnumerable<Person>> GetOldest()
        {
            return await Task.FromResult(new List<Person> { _persons.OrderBy(p => p.DateOfBirth).First() }.AsEnumerable());
        }

        public async Task<IEnumerable<Person>> GetByBirthYear(int year)
        {
            return await Task.FromResult(_persons.Where(p => p.DateOfBirth.Year == year).ToList().AsEnumerable());
        }

        public async Task<IEnumerable<Person>> GetByBirthYearGreaterThan(int year)
        {
            return await Task.FromResult(_persons.Where(p => p.DateOfBirth.Year > year).ToList().AsEnumerable());
        }

        public async Task<IEnumerable<Person>> GetByBirthYearLessThan(int year)
        {
            return await Task.FromResult(_persons.Where(p => p.DateOfBirth.Year < year).ToList().AsEnumerable());
        }
    }
}

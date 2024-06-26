using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;
using OfficeOpenXml;

namespace WebApplication.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        // GET: Person
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View("Index", await _personService.Get());
        }

        // GET: Person/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var person = await _personService.Get(id);
            if (person == null)
            {
                return NotFound();
            }
            return View("Details", person);
        }

        // GET: Person/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            await _personService.Add(person);
            return RedirectToAction("Get", new { id = person.Id });
        }

        // GET: Person/Edit/{id}
        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var person = await _personService.Get(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/{id}
        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }
            await _personService.Update(person);
            return RedirectToAction("Get", new { id = person.Id });
        }

        // GET: Person/Delete/{id}
        [HttpGet("{id}/delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var person = await _personService.Get(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/{id}
        [HttpPost("{id}/delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmesPost(Guid id)
        {
            var person = await _personService.Get(id);
            if (person == null)
            {
                return NotFound();
            }
            await _personService.Delete(id);
            return RedirectToAction("Get");
        }

        public async Task<IActionResult> GetMales()
        {
            var males = await _personService.GetMales();
            return View("Index", males);
        }

        public async Task<IActionResult> GetOldest()
        {
            var oldest = await _personService.GetOldest();
            return View("Index", oldest);
        }

        public async Task<IActionResult> GetByBirthYear(int year)
        {
            var Persons = await _personService.GetByBirthYear(year);
            return View("Index", Persons);
        }

        public async Task<IActionResult> GetByBirthYearGreaterThan(int year)
        {
            var Persons = await _personService.GetByBirthYearGreaterThan(year);
            return View("Index", Persons);
        }

        public async Task<IActionResult> GetByBirthYearLessThan(int year)
        {
            var Persons = await _personService.GetByBirthYearLessThan(year);
            return View("Index", Persons);
        }

        public async Task<IActionResult> DownloadExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Persons");
                worksheet.Cells.LoadFromCollection(await _personService.Get(), PrintHeaders: true);

                package.Save();
            }

            stream.Position = 0;
            string excelName = "Persons.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}

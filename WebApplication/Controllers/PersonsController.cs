using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;
using OfficeOpenXml;

namespace WebApplication.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonService _service;

        public PersonsController(IPersonService service)
        {
            _service = service;
        }

        // GET: /persons
        public async Task<IActionResult> Index()
        {
            var persons = await _service.GetPersons();
            return View(persons);
        }

        // GET: /persons/details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var person = await _service.GetPerson(id);
                return View(person);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: /persons/edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var person = await _service.GetPerson(id);
                return View(person);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: /persons/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.PutPerson(id, person);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: /persons/create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /persons/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _service.PostPerson(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: /persons/delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var person = await _service.GetPerson(id);
                return View(person);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: /persons/delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _service.DeletePerson(id);
                TempData["StatusMessage"] = "Person deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "Something went wrong: " + ex.Message;
                return NotFound(ex.Message);
            }
        }

        public async Task<IActionResult> GetMales()
        {
            var males = await _service.GetMales();
            return View("Index", males);
        }

        public async Task<IActionResult> GetOldest()
        {
            var oldest = await _service.GetOldest();
            return View("Index", oldest);
        }

        public async Task<IActionResult> GetByBirthYear(int year)
        {
            var persons = await _service.GetByBirthYear(year);
            return View("Index", persons);
        }

        public async Task<IActionResult> GetByBirthYearGreaterThan(int year)
        {
            var persons = await _service.GetByBirthYearGreaterThan(year);
            return View("Index", persons);
        }

        public async Task<IActionResult> GetByBirthYearLessThan(int year)
        {
            var persons = await _service.GetByBirthYearLessThan(year);
            return View("Index", persons);
        }

        public async Task<IActionResult> ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Persons");
                worksheet.Cells.LoadFromCollection(await _service.GetPersons(), PrintHeaders: true);

                package.Save();
            }

            stream.Position = 0;
            string excelName = "Persons.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
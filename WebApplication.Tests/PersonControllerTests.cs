using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Controllers;
using WebApplication.Services;

namespace WebApplication.Tests
{
    public class PersonsControllerTests
    {
        private Mock<IPersonService> _serviceMock;
        private PersonsController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IPersonService>();
            _controller = new PersonsController(_serviceMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task Index_WhenCalled_ReturnsViewResultWithListOfPersons()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _serviceMock.Setup(s => s.GetPersons()).ReturnsAsync(persons);

            // Act
            var result = await _controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(persons));
        }

        [Test]
        public async Task Details_WhenCalled_ReturnsViewResultWithPerson()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetPerson(id)).ReturnsAsync(person);

            // Act
            var result = await _controller.Details(id);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(person));
        }

        [Test]
        public async Task Edit_WhenCalled_ReturnsViewResultWithPerson()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetPerson(id)).ReturnsAsync(person);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(person));
        }

        [Test]
        public async Task Create_WhenModelStateIsValid_ReturnsRedirectToActionResult()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            _serviceMock.Setup(s => s.PostPerson(person)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(person);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public async Task Delete_WhenCalled_ReturnsViewResultWithPerson()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetPerson(id)).ReturnsAsync(person);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(person));
        }

        [Test]
        public async Task GetMales_WhenCalled_ReturnsViewResultWithListOfMales()
        {
            // Arrange
            var males = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _serviceMock.Setup(s => s.GetMales()).ReturnsAsync(males);

            // Act
            var result = await _controller.GetMales();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(males));
        }

        [Test]
        public async Task GetOldest_WhenCalled_ReturnsViewResultWithOldestPerson()
        {
            // Arrange
            var oldest = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _serviceMock.Setup(s => s.GetOldest()).ReturnsAsync(oldest);

            // Act
            var result = await _controller.GetOldest();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(oldest));
        }

        [Test]
        public async Task GetByBirthYear_WhenCalled_ReturnsViewResultWithPersons()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            var year = 2000;
            _serviceMock.Setup(s => s.GetByBirthYear(year)).ReturnsAsync(persons);

            // Act
            var result = await _controller.GetByBirthYear(year);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(persons));
        }

        [Test]
        public async Task GetByBirthYearGreaterThan_WhenCalled_ReturnsViewResultWithPersons()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            var year = 2001;
            _serviceMock.Setup(s => s.GetByBirthYearGreaterThan(year)).ReturnsAsync(persons);

            // Act
            var result = await _controller.GetByBirthYearGreaterThan(year);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(persons));
        }

        [Test]
        public async Task GetByBirthYearLessThan_WhenCalled_ReturnsViewResultWithPersons()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            var year = 1999;
            _serviceMock.Setup(s => s.GetByBirthYearLessThan(year)).ReturnsAsync(persons);

            // Act
            var result = await _controller.GetByBirthYearLessThan(year);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(persons));
        }


        [Test]
        public async Task ExportToExcel_WhenCalled_ReturnsFileResult()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _serviceMock.Setup(s => s.GetPersons()).ReturnsAsync(persons);

            // Act
            var result = await _controller.ExportToExcel();

            // Assert
            Assert.That(result, Is.InstanceOf<FileResult>());
        }
    }
}

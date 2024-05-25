using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.Repositories;

namespace WebApplication.Tests
{
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> _repositoryMock;
        private PersonService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPersonRepository>();
            _service = new PersonService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetPersons_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _repositoryMock.Setup(repo => repo.GetPersons()).ReturnsAsync(persons);

            // Act
            var result = await _service.GetPersons();

            // Assert
            Assert.That(result, Is.EqualTo(persons));
        }

        [Test]
        public async Task GetPerson_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            var id = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetPerson(id)).ReturnsAsync(person);

            // Act
            var result = await _service.GetPerson(id);

            // Assert
            Assert.That(result, Is.EqualTo(person));
        }

        [Test]
        public async Task PutPerson_WhenCalled_ExecutesSuccessfully()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            var id = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.PutPerson(id, person)).Returns(Task.CompletedTask);

            // Act
            await _service.PutPerson(id, person);

            // Assert
            _repositoryMock.Verify(repo => repo.PutPerson(id, person), Times.Once);
        }

        [Test]
        public async Task PostPerson_WhenCalled_ExecutesSuccessfully()
        {
            // Arrange
            var person = new Person { FirstName = "firstName", LastName = "lastName", PhoneNumber = "xxxx.xxx.xxx" };
            _repositoryMock.Setup(repo => repo.PostPerson(person)).Returns(Task.CompletedTask);

            // Act
            await _service.PostPerson(person);

            // Assert
            _repositoryMock.Verify(repo => repo.PostPerson(person), Times.Once);
        }

        [Test]
        public async Task DeletePerson_WhenCalled_ExecutesSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.DeletePerson(id)).Returns(Task.CompletedTask);

            // Act
            await _service.DeletePerson(id);

            // Assert
            _repositoryMock.Verify(repo => repo.DeletePerson(id), Times.Once);
        }

        [Test]
        public async Task GetMales_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var males = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _repositoryMock.Setup(repo => repo.GetMales()).ReturnsAsync(males);

            // Act
            var result = await _service.GetMales();

            // Assert
            Assert.That(result, Is.EqualTo(males));
        }

        [Test]
        public async Task GetOldest_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var oldest = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            _repositoryMock.Setup(repo => repo.GetOldest()).ReturnsAsync(oldest);

            // Act
            var result = await _service.GetOldest();

            // Assert
            Assert.That(result, Is.EqualTo(oldest));
        }

        [Test]
        public async Task GetByBirthYear_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            var year = 2000;
            _repositoryMock.Setup(repo => repo.GetByBirthYear(year)).ReturnsAsync(persons);

            // Act
            var result = await _service.GetByBirthYear(year);

            // Assert
            Assert.That(result, Is.EqualTo(persons));
        }

        [Test]
        public async Task GetByBirthYearGreaterThan_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            var year = 2001;
            _repositoryMock.Setup(repo => repo.GetByBirthYearGreaterThan(year)).ReturnsAsync(persons);

            // Act
            var result = await _service.GetByBirthYearGreaterThan(year);

            // Assert
            Assert.That(result, Is.EqualTo(persons));
        }

        [Test]
        public async Task GetByBirthYearLessThan_WhenCalled_ReturnsExpectedResult()
        {
            // Arrange
            var persons = new List<Person>
                {
                    new() { FirstName = "firstName 1", LastName = "lastName1", PhoneNumber = "xxxx.xxx.xx1"},
                    new() { FirstName = "firstName 2", LastName = "lastName2", PhoneNumber = "xxxx.xxx.xx2"},
                };
            var year = 1999;
            _repositoryMock.Setup(repo => repo.GetByBirthYearLessThan(year)).ReturnsAsync(persons);

            // Act
            var result = await _service.GetByBirthYearLessThan(year);

            // Assert
            Assert.That(result, Is.EqualTo(persons));
        }
    }
}

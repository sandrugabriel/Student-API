using Moq;
using StudentApi.Dto;
using StudentApi.Exceptions;
using StudentApi.Service.interfaces;
using StudentApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApi.Constants;
using StudentApi.Models;
using Teste.Helpers;
using StudentApi.Repository.interfaces;

namespace Teste.UnitTests
{
    public class TestCommandService
    {

        Mock<IRepositoryStudent> _mock;
        ICommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IRepositoryStudent>();
            _service = new CommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidAge()
        {

            var create = new CreateRequest
            {
                Age = 0,
                Grade = 1,
                Name = "test"
            };

            _mock.Setup(repo => repo.Create(create)).ReturnsAsync((Student)null);

            var exception = await Assert.ThrowsAsync<InvalidAge>(() => _service.Create(create));

            Assert.Equal(Constants.InvalidAge, exception.Message);


        }

        [Fact]
        public async Task Create_ReturnStudent()
        {
            var create = new CreateRequest
            {
                Age = 10,
                Grade = 1,
                Name = "test"
            };

            var student = TestStudentFactory.CreateStudent(5);
            student.Age = create.Age;
            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(student);

            var result = await _service.Create(create);

            Assert.NotNull(result);

            Assert.Equal(result, student);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
                Age = 20
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Student)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Update(1, update));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidAge()
        {
            var update = new UpdateRequest
            {
                Age = 0
            };

            var student = TestStudentFactory.CreateStudent(5);
            student.Age = update.Age.Value;


            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(student);

            var exception = await Assert.ThrowsAsync<InvalidAge>(() => _service.Update(5, update));

            Assert.Equal(Constants.InvalidAge, exception.Message);
        }

        [Fact]
        public async Task Update_ValidAge()
        {
            var update = new UpdateRequest
            {
                Age = 2010
            };

            var student = TestStudentFactory.CreateStudent(5);
            student.Age = update.Age.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(student);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(student);

            var result = await _service.Update(5, update);

            Assert.NotNull(result);
            Assert.Equal(student, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Student)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Delete(5));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task Delete_ValidAge()
        {
            var student = TestStudentFactory.CreateStudent(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);

            var result = await _service.Delete(1);

            Assert.NotNull(result);
            Assert.Equal(student, result);
        }

    }
}

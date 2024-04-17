using Moq;
using StudentApi.Exceptions;
using StudentApi.Service.interfaces;
using StudentApi.Service;
using StudentApi.Repository.interfaces;
using StudentApi.Models;
using StudentApi.Constants;
using Teste.Helpers;

namespace Teste.UnitTests
{
    public class TestQueryService
    {

        Mock<IRepositoryStudent> _mock;
        IQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepositoryStudent>();
            _service = new QueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Student>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.ItemsDoNotExist);

        }

        [Fact]
        public async Task GetAll_ReturnAllStudent()
        {
            var student = TestStudentFactory.CreateStudents(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(student);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Contains(student[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Student)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetById_ReturnStudent()
        {

            var movie = TestStudentFactory.CreateStudent(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(movie);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(movie, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Student)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByNameAsync(""));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ReturnStudent()
        {
            var movie = TestStudentFactory.CreateStudent(3);

            movie.Name = "test";
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(movie);

            var result = await _service.GetByNameAsync("test");

            Assert.NotNull(result);
            Assert.Equal(movie, result);
        }

    }
}
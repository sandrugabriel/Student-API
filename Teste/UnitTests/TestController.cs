using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentApi.Constants;
using StudentApi.Controllers;
using StudentApi.Controllers.interfaces;
using StudentApi.Dto;
using StudentApi.Exceptions;
using StudentApi.Models;
using StudentApi.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;

namespace Teste.UnitTests
{
    public class TestController
    {

        Mock<ICommandService> _command;
        Mock<IQueryService> _query;
        ControllerAPI _controller;

        public TestController()
        {
            _command = new Mock<ICommandService>();
            _query = new Mock<IQueryService>();
            _controller = new ControllerStudent(_query.Object, _command.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.ItemsDoNotExist, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var students = TestStudentFactory.CreateStudents(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(students);

            var result = await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var studentsAll = Assert.IsType<List<Student>>(okresult.Value);

            Assert.Equal(5, studentsAll.Count);
            Assert.Equal(200, okresult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateRequest
            {
                Age = 0,
                Grade = 1,
                Name = "test"
            };

            _command.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ThrowsAsync(new InvalidAge(Constants.InvalidAge));

            var result = await _controller.CreateStudent(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.InvalidAge, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var create = new CreateRequest
            {
                Age = 20,
                Grade = 1,
                Name = "test"
            };
            var student = TestStudentFactory.CreateStudent(5);
            student.Age = create.Age;

            _command.Setup(repo => repo.Create(create)).ReturnsAsync(student);

            var result = await _controller.CreateStudent(create);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(student, okResult.Value);

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

            _command.Setup(repo => repo.Update(5, update)).ThrowsAsync(new InvalidAge(Constants.InvalidAge));

            var result = await _controller.UpdateStudent(5, update);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 400);
            Assert.Equal(bad.Value, Constants.InvalidAge);


        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Age = 200
            };
            var student = TestStudentFactory.CreateStudent(5);
            student.Age = update.Age.Value;

            _command.Setup(repo => repo.Update(5, update)).ReturnsAsync(student);

            var result = await _controller.UpdateStudent(5, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, student);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _command.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.DeleteStudent(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var student = TestStudentFactory.CreateStudent(1);

            _command.Setup(repo => repo.Delete(1)).ReturnsAsync(student);

            var result = await _controller.DeleteStudent(1);

            var okReult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okReult.StatusCode);
            Assert.Equal(student, okReult.Value);

        }

    }
}

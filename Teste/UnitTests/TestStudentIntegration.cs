using Newtonsoft.Json;
using StudentApi.Dto;
using StudentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teste.Helpers;
using Teste.Infrastructure;

namespace Teste.UnitTests
{
    public class TestStudentIntegration : IClassFixture<ApiWebApplicationFactory>
    {

        private readonly HttpClient _client;

        public TestStudentIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllStudents_StudentsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createStudentRequest = TestStudentFactory.CreateStudent(1);
            var content = new StringContent(JsonConvert.SerializeObject(createStudentRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerStudent/CreateStudent", content);

            var response = await _client.GetAsync("/api/v1/ControllerStudent/All");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStudentById_StudentFound_ReturnsOkStatusCode()
        {
            var createStudentRequest = new CreateRequest
            { Age = 10, Grade = 2,Name="Asdasd"};
            var content = new StringContent(JsonConvert.SerializeObject(createStudentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerStudent/CreateStudent", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Student>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name, createStudentRequest.Name);
        }

        [Fact]
        public async Task GetStudentById_StudentNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Student/FindById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerStudent/CreateStudent";
            var createStudentRequest = new CreateRequest
            { Age = 10, Grade = 2, Name = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createStudentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Student>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createStudentRequest.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerStudent/createStudent";
            var createStudent = new CreateRequest
            { Age = 10, Grade = 2, Name = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createStudent), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Student>(responseString);

            request = $"/api/v1/ControllerStudent/UpdateStudent?id={result.Id}";
            var updateStudent = new UpdateRequest { Name = "12test" };
            content = new StringContent(JsonConvert.SerializeObject(updateStudent), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Student>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, updateStudent.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidStudentDate_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerStudent/CreateStudent";
            var createStudent = new CreateRequest
            { Age = 10, Grade = 2, Name = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createStudent), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Student>(responseString);

            request = $"/api/v1/ControllerStudent/UpdateStudent?id={result.Id}";
            var updateStudent = new UpdateRequest { Age = 0 };
            content = new StringContent(JsonConvert.SerializeObject(updateStudent), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Student>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Name, updateStudent.Name);
        }

        [Fact]
        public async Task Put_Update_StudentDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerStudent/UpdateStudent";
            var updateStudent = new UpdateRequest { Name = "asd" };
            var content = new StringContent(JsonConvert.SerializeObject(updateStudent), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_StudentExists_ReturnsDeletedStudent()
        {
            var request = "/api/v1/ControllerStudent/CreateStudent";
            var createStudent = new CreateRequest
            { Age = 10, Grade = 2, Name = "Asdasd" };
            var content = new StringContent(JsonConvert.SerializeObject(createStudent), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Student>(responseString)!;

            request = $"/api/v1/ControllerStudent/DeleteStudent?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Student>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, createStudent.Name);
        }

        [Fact]
        public async Task Delete_Delete_StudentDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerStudent/DeleteStudent?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

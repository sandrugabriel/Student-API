using Microsoft.AspNetCore.Mvc;
using StudentApi.Dto;
using StudentApi.Models;
using StudentApi.Repository.interfaces;
using System;

namespace StudentApi.Controllers
{

    [ApiController]
    [Route("api/v1/students")]
    public class ControllerStudent : ControllerBase
    {

        private readonly ILogger<ControllerStudent> _logger;

        private IRepositoryStudent _Repository;

        public ControllerStudent(ILogger<ControllerStudent> logger, IRepositoryStudent Repository)
        {
            _logger = logger;
            _Repository = Repository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var products = await _Repository.GetAllAsync();
            return Ok(products);
        }


        [HttpGet("/findById")]
        public async Task<ActionResult<Student>> GetById([FromQuery] int id)
        {
            var student = await _Repository.GetByIdAsync(id);
            return Ok(student);
        }


        [HttpGet("/find/{name}")]
        public async Task<ActionResult<Student>> GetByNameRoute([FromRoute] string name)
        {
            var student = await _Repository.GetByNameAsync(name);
            return Ok(student);
        }


        [HttpPost("/create")]
        public async Task<ActionResult<Student>> Create([FromBody] CreateRequest request)
        {
            var student = await _Repository.Create(request);
            return Ok(student);

        }

        [HttpPut("/update")]
        public async Task<ActionResult<Student>> Update([FromQuery] int id, [FromBody] UpdateRequest request)
        {
            var student = await _Repository.Update(id, request);
            return Ok(student);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<Student>> DeleteCarById([FromQuery] int id)
        {
            var student = await _Repository.DeleteById(id);
            return Ok(student);
        }


    }
}

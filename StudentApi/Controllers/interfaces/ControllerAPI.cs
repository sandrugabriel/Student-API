using Microsoft.AspNetCore.Mvc;
using StudentApi.Dto;
using StudentApi.Models;

namespace StudentApi.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ControllerAPI : ControllerBase
    {


        [HttpGet("/all")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Student>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Student>>> GetAll();

        [HttpGet("/findById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Student))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Student>> GetById([FromQuery] int id);

        [HttpGet("/findByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Student))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Student>> GetByName([FromQuery] string name);

        [HttpPost("/createStudent")]
        [ProducesResponseType(statusCode: 201, type: typeof(Student))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Student>> CreateStudent(CreateRequest request);

        [HttpPut("/updateStudent")]
        [ProducesResponseType(statusCode: 200, type: typeof(Student))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Student>> UpdateStudent([FromQuery] int id, UpdateRequest request);

        [HttpDelete("/deleteStudent")]
        [ProducesResponseType(statusCode: 200, type: typeof(Student))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Student>> DeleteStudent([FromQuery] int id);
    }
}

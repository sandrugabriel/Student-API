using Microsoft.AspNetCore.Mvc;
using StudentApi.Controllers.interfaces;
using StudentApi.Dto;
using StudentApi.Exceptions;
using StudentApi.Models;
using StudentApi.Repository.interfaces;
using StudentApi.Service.interfaces;
using System;

namespace StudentApi.Controllers
{

    public class ControllerStudent : ControllerAPI
    {


        private IQueryService _queryService;
        private ICommandService _commandService;

        public ControllerStudent(IQueryService queryService, ICommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Student>>> GetAll()
        {
            try
            {
                var students = await _queryService.GetAll();

                return Ok(students);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Student>> GetByName([FromQuery] string name)
        {

            try
            {
                var student = await _queryService.GetByNameAsync(name);
                return Ok(student);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Student>> GetById([FromQuery] int id)
        {

            try
            {
                var student = await _queryService.GetById(id);
                return Ok(student);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Student>> CreateStudent(CreateRequest request)
        {
            try
            {
                var student = await _commandService.Create(request);
                return Ok(student);
            }
            catch (InvalidAge ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Student>> UpdateStudent([FromQuery] int id, UpdateRequest request)
        {
            try
            {
                var student = await _commandService.Update(id, request);
                return Ok(student);
            }
            catch (InvalidAge ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Student>> DeleteStudent([FromQuery] int id)
        {
            try
            {
                var student = await _commandService.Delete(id);
                return Ok(student);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}

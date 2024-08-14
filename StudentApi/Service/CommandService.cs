using StudentApi.Dto;
using StudentApi.Exceptions;
using StudentApi.Models;
using StudentApi.Repository.interfaces;
using StudentApi.Service.interfaces;

namespace StudentApi.Service
{
    public class CommandService : ICommandService
    {


        private IRepositoryStudent _repository;

        public CommandService(IRepositoryStudent repository)
        {
            _repository = repository;
        }

        public async Task<Student> Create(CreateRequest request)
        {

            if (request.Age <= 0)
            {
                throw new InvalidAge(Constants.Constants.InvalidAge);
            }

            var student = await _repository.Create(request);

            return student;
        }

        public async Task<Student> Update(int id, UpdateRequest request)
        {

            var student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (request.Age <= 0)
            {
                throw new InvalidAge(Constants.Constants.InvalidAge);
            }
            student = await _repository.Update(id, request);
            return student;
        }

        public async Task<Student> Delete(int id)
        {

            var student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);
            return student;
        }
    }
}

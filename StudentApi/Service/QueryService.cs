using StudentApi.Exceptions;
using StudentApi.Models;
using StudentApi.Repository.interfaces;
using StudentApi.Service.interfaces;

namespace StudentApi.Service
{
    public class QueryService : IQueryService
    {
        private IRepositoryStudent _repository;

        public QueryService(IRepositoryStudent repository)
        {
            _repository = repository;
        }

        public async Task<List<Student>> GetAll()
        {
            var student = await _repository.GetAllAsync();

            if (student.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Student>)student;
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            var student = await _repository.GetByNameAsync(name);

            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return student;
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return student;
        }
    }
}

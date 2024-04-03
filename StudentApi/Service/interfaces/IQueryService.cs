using StudentApi.Models;

namespace StudentApi.Service.interfaces
{
    public interface IQueryService
    {
        Task<List<Student>> GetAll();

        Task<Student> GetById(int id);

        Task<Student> GetByNameAsync(string name);
    }
}

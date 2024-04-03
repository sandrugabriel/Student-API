using StudentApi.Dto;
using StudentApi.Models;

namespace StudentApi.Service.interfaces
{
    public interface ICommandService
    {
        Task<Student> Create(CreateRequest request);

        Task<Student> Update(int id, UpdateRequest request);

        Task<Student> Delete(int id);
    }
}

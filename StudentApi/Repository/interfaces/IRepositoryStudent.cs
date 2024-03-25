using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using System;

namespace StudentApi.Repository.interfaces
{
    public interface IRepositoryStudent
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetByNameAsync(string name);

        Task<Student> GetByIdAsync(int id);
    }
}

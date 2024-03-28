using Microsoft.AspNetCore.Mvc;
using StudentApi.Dto;
using StudentApi.Models;
using System;

namespace StudentApi.Repository.interfaces
{
    public interface IRepositoryStudent
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetByNameAsync(string name);

        Task<Student> GetByIdAsync(int id);


        Task<Student> Create(CreateRequest request);

        Task<Student> Update(int id, UpdateRequest request);

        Task<Student> DeleteById(int id);
    }
}

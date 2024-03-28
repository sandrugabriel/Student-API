using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Dto;
using StudentApi.Models;
using StudentApi.Repository.interfaces;
using System;

namespace StudentApi.Repository
{

    public class RepositoryStudent : IRepositoryStudent
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryStudent(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Student.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            List<Student> cars = await _context.Student.ToListAsync();

            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Id == id) return cars[i];
            }

            return null;
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            List<Student> allcars = await _context.Student.ToListAsync();

            for (int i = 0; i < allcars.Count; i++)
            {
                if (allcars[i].Name.Equals(name))
                {
                    return allcars[i];
                }
            }

            return null;
        }


        public async Task<Student> Create(CreateRequest request)
        {

            var student = _mapper.Map<Student>(request);

            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            return student;

        }

        public async Task<Student> Update(int id, UpdateRequest request)
        {

            var student = await _context.Student.FindAsync(id);

            student.Name = request.Name ?? student.Name;
            student.Age = request.Age ?? student.Age;
            student.Grade = request.Grade ?? student.Grade;

            _context.Student.Update(student);

            await _context.SaveChangesAsync();

            return student;

        }

        public async Task<Student> DeleteById(int id)
        {
            var student = await _context.Student.FindAsync(id);

            _context.Student.Remove(student);

            await _context.SaveChangesAsync();

            return student;
        }

    }
}

using StudentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Helpers
{
    public class TestStudentFactory
    {
        public static Student CreateStudent(int id)
        {
            return new Student
            {
                Id = id,
                Age = 10 + id,
                Grade = 3 + id,
                Name = "test" + id
            };
        }

            public static List<Student> CreateStudents(int count)
            {
                List<Student> movies = new List<Student>();
                for (int i = 1; i <= count; i++)
                {
                    movies.Add(CreateStudent(i));
                }

                return movies;
            }

    }
}

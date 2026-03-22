using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data.Context;
using University.Data.Entities;

namespace University.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityDbContext _context;

        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

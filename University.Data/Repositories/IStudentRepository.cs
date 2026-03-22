using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data.Entities;

namespace University.Data.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
        void SaveChanges();
    }
}

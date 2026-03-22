using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services
{
    public interface IStudentService
    {
        List<StudentDto> GetAll();
        StudentDto? GetById(int id);
        void Create(CreateStudentForm form);
        void Update(int id, UpdateStudentForm form);
        void Delete(int id);
    }
}

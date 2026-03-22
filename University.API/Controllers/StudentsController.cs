using Microsoft.AspNetCore.Mvc;
using University.Core.DTOs;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {
            var students = _studentService.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetStudentById(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentForm form)
        {
            _studentService.Create(form);
            return Ok("Student created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentForm form)
        {
            _studentService.Update(id, form);
            return Ok("Student updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.Delete(id);
            return Ok("Student deleted successfully");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCourseManagement.Models
{
    public class Course
    { public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
         public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
          
        public int TeacherId { get; set; }
         public User Teacher { get; set; } = null!;
        public ICollection<Assignment> Assignments { get; set; } = [];
        public Syllabus Syllabus { get; set; } = null!;
    }
}

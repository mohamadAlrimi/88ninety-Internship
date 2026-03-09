using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCourseManagement.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        public string AssignmentTitle { get; set; } = null!;

        public string? Description { get; set; }
        
        public float Weight { get; set; }

        public int MaxGrade { get; set; }

        public DateTime DueDate { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Grade> Grades { get; set; } = [];
    }
}

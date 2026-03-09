using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCourseManagement.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        public float Score { get; set; }

        // Foreign Key
        public int AssignmentId { get; set; }

        public int StudentId { get; set; }
        // Navigation Property 
        public Assignment Assignment { get; set; } = null!;
        public User Student { get; set; } = null!;
    }
}

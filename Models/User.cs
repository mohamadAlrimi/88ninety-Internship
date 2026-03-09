using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCourseManagement.Models
{
    public class User
    {   public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Role { get; set; } = null!;

        public ICollection<Course> TaughtCourses { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Grade> Grades { get; set; } = [];
            



        
    }
}

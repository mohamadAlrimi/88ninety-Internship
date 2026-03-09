using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCourseManagement.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? CommentContent { get; set; }

       // Foreign Key
        public int AssignmentId { get; set; }
        public int CreatedByUserId { get; set; }
         // Navigation Property
        public Assignment Assignment { get; set; } = null!;

       
        public User CreatedByUser { get; set; } = null!;
    }
}


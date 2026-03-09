using UniversityCourseManagement.Data;
using UniversityCourseManagement.Models;

namespace UniversityCourseManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new UniversityDbContext();

            SeedUsers(context);
            SeedCoursesAndSyllabi(context);
            SeedAssignments(context);
            SeedComments(context);
            SeedGrades(context);

            Console.WriteLine("Data inserted successfully.");
            Console.WriteLine();

            ShowAllCourses(context);
            Console.WriteLine();

            ShowAllStudents(context);
            Console.WriteLine();

            ShowAssignmentsForCourse(context, 1);
            Console.WriteLine();

            ShowCommentsForAssignment(context, 1);
            Console.WriteLine();

            ShowGradesForStudent(context, 3);
            Console.WriteLine();

            ShowAssignmentsWithCourseAndTeacher(context);
            Console.WriteLine();

            ShowAverageGradePerCourse(context);
            Console.WriteLine();

            Console.WriteLine("Letter Grade for 85 = " + GetLetterGrade(85));
            Console.WriteLine("GPA for student 3 = " + CalculateGPA(context, 3));

            UpdateStudentRoleToTeacher(context, 3);
            Console.WriteLine();

            DeleteComment(context, 1);
            Console.WriteLine();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }




        //LINQ START 
        static void ShowAllCourses(UniversityDbContext context)
        {
            var courses = context.Courses.ToList();

            Console.WriteLine("All Courses:");
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.CourseId} - {course.CourseName}");
            }
        }

        static void ShowAssignmentsForCourse(UniversityDbContext context, int courseId)
        {
            var assignments = context.Assignments
                .Where(a => a.CourseId == courseId)
                .ToList();

            Console.WriteLine($"Assignments for Course ID {courseId}:");
            foreach (var assignment in assignments)
            {
                Console.WriteLine($"{assignment.AssignmentTitle} - Due: {assignment.DueDate}");
            }
        }

        static void ShowAllStudents(UniversityDbContext context)
        {
            var students = context.Users
                .Where(u => u.Role == "Student")
                .ToList();

            Console.WriteLine("All Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.UserId} - {student.FirstName} {student.LastName}");
            }
        }
        static void ShowCommentsForAssignment(UniversityDbContext context, int assignmentId)
        {
            var comments = context.Comments
                .Where(c => c.AssignmentId == assignmentId)
                .Select(c => new
                {
                    c.CommentContent,
                    c.CreatedDate,
                    UserName = c.CreatedByUser.FirstName + " " + c.CreatedByUser.LastName
                })
                .ToList();

            Console.WriteLine($"Comments for Assignment ID {assignmentId}:");
            foreach (var comment in comments)
            {
                Console.WriteLine($"{comment.UserName}: {comment.CommentContent} - {comment.CreatedDate}");
            }
        }
        static void ShowGradesForStudent(UniversityDbContext context, int studentId)
        {
            var grades = context.Grades
                .Where(g => g.StudentId == studentId)
                .Select(g => new
                {
                    AssignmentTitle = g.Assignment.AssignmentTitle,
                    Score = g.Score
                })
                .ToList();

            Console.WriteLine($"Grades for Student ID {studentId}:");
            foreach (var grade in grades)
            {
                Console.WriteLine($"{grade.AssignmentTitle} - {grade.Score}");
            }
        }
        static void ShowAssignmentsWithCourseAndTeacher(UniversityDbContext context)
        {
            var data = context.Assignments
                .Select(a => new
                {
                    AssignmentTitle = a.AssignmentTitle,
                    CourseName = a.Course.CourseName,
                    TeacherName = a.Course.Teacher.FirstName + " " + a.Course.Teacher.LastName
                })
                .ToList();

            Console.WriteLine("Assignments with Course and Teacher:");
            foreach (var item in data)
            {
                Console.WriteLine($"{item.AssignmentTitle} - {item.CourseName} - {item.TeacherName}");
            }
        }
        static void ShowAverageGradePerCourse(UniversityDbContext context)
        {
            var averages = context.Grades
                .GroupBy(g => g.Assignment.Course.CourseName)
                .Select(g => new
                {
                    CourseName = g.Key,
                    AverageGrade = g.Average(x => x.Score)
                })
                .ToList();

            Console.WriteLine("Average Grade Per Course:");
            foreach (var item in averages)
            {
                Console.WriteLine($"{item.CourseName} - {item.AverageGrade:F2}");
            }
        }
        static string GetLetterGrade(float score)
        {
            if (score >= 90)
                return "A";
            else if (score >= 80)
                return "B";
            else if (score >= 70)
                return "C";
            else if (score >= 60)
                return "D";
            else
                return "F";
        }

        static double CalculateGPA(UniversityDbContext context, int studentId)
        {
            var grades = context.Grades
                .Where(g => g.StudentId == studentId)
                .Select(g => g.Score)
                .ToList();

            if (!grades.Any())
                return 0;

            double totalPoints = 0;

            foreach (var score in grades)
            {
                if (score >= 90)
                    totalPoints += 4.0;
                else if (score >= 80)
                    totalPoints += 3.0;
                else if (score >= 70)
                    totalPoints += 2.0;
                else if (score >= 60)
                    totalPoints += 1.0;
                else
                    totalPoints += 0.0;
            }

            return totalPoints / grades.Count;
        }
        //LINQ END

        //Update a student’s role to Teacher
        static void UpdateStudentRoleToTeacher(UniversityDbContext context, int studentId)
        {
            var student = context.Users.FirstOrDefault(u => u.UserId == studentId);

            if (student != null)
            {
                student.Role = "Teacher";
                context.SaveChanges();
                Console.WriteLine("Student role updated to Teacher.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        //Delete a specific comment
        static void DeleteComment(UniversityDbContext context, int commentId)
        {
            var comment = context.Comments.FirstOrDefault(c => c.CommentId == commentId);

            if (comment != null)
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
                Console.WriteLine("Comment deleted successfully.");
            }
            else
            {
                Console.WriteLine("Comment not found.");
            }
        }

        static void SeedUsers(UniversityDbContext context)
        {
            if (context.Users.Any())
                return;

            var users = new List<User>
            {
                new User
                {
                    UserName = "sami.teacher",
                    FirstName = "Sami",
                    LastName = "Ahmad",
                    EmailAddress = "sami@uni.com",
                    PhoneNumber = "1111111111",
                    Role = "Teacher"
                },
                new User
                {
                    UserName = "feryal.teacher",
                    FirstName = "Feryal",
                    LastName = "Ali",
                    EmailAddress = "feryal@uni.com",
                    PhoneNumber = "2222222222",
                    Role = "Teacher"
                },
                new User
                {
                    UserName = "intern1",
                    FirstName = "Omar",
                    LastName = "Hassan",
                    EmailAddress = "omar@uni.com",
                    PhoneNumber = "3333333333",
                    Role = "Student"
                },
                new User
                {
                    UserName = "intern2",
                    FirstName = "Lina",
                    LastName = "Yousef",
                    EmailAddress = "lina@uni.com",
                    PhoneNumber = "4444444444",
                    Role = "Student"
                },
                new User
                {
                    UserName = "intern3",
                    FirstName = "Ahmad",
                    LastName = "Khaled",
                    EmailAddress = "ahmad@uni.com",
                    PhoneNumber = "5555555555",
                    Role = "Student"
                },
                new User
                {
                    UserName = "intern4",
                    FirstName = "Maya",
                    LastName = "Saleh",
                    EmailAddress = "maya@uni.com",
                    PhoneNumber = "6666666666",
                    Role = "Student"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        static void SeedCoursesAndSyllabi(UniversityDbContext context)
        {
            if (context.Courses.Any())
                return;

            var sami = context.Users.First(u => u.FirstName == "Sami");
            var feryal = context.Users.First(u => u.FirstName == "Feryal");

            var courses = new List<Course>
            {
                new Course
                {
                    CourseName = "SQL",
                    StartDate = DateTime.Now.AddDays(-20),
                    EndDate = DateTime.Now.AddMonths(2),
                    TeacherId = sami.UserId
                },
                new Course
                {
                    CourseName = "C#",
                    StartDate = DateTime.Now.AddDays(-15),
                    EndDate = DateTime.Now.AddMonths(2),
                    TeacherId = sami.UserId
                },
                new Course
                {
                    CourseName = "Entity Framework",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddMonths(3),
                    TeacherId = feryal.UserId
                },
                new Course
                {
                    CourseName = "Web API",
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddMonths(3),
                    TeacherId = feryal.UserId
                },
                new Course
                {
                    CourseName = "React",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(4),
                    TeacherId = sami.UserId
                }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var syllabi = context.Courses
                .Select(c => new Syllabus
                {
                    CourseId = c.CourseId,
                    Content = $"This is the syllabus for {c.CourseName}"
                })
                .ToList();

            context.Syllabi.AddRange(syllabi);
            context.SaveChanges();
        }

        static void SeedAssignments(UniversityDbContext context)
        {
            if (context.Assignments.Any())
                return;

            var random = new Random();
            var assignments = new List<Assignment>();
            var courses = context.Courses.ToList();

            foreach (var course in courses)
            {
                for (int i = 1; i <= 5; i++)
                {
                    assignments.Add(new Assignment
                    {
                        AssignmentTitle = $"{course.CourseName} Assignment {i}",
                        Description = $"Description for {course.CourseName} Assignment {i}",
                        Weight = 10,
                        MaxGrade = 100,
                        DueDate = DateTime.Now.AddDays(random.Next(-15, 15)),
                        CourseId = course.CourseId
                    });
                }
            }

            context.Assignments.AddRange(assignments);
            context.SaveChanges();
        }

        static void SeedComments(UniversityDbContext context)
        {
            if (context.Comments.Any())
                return;

            var random = new Random();
            var comments = new List<Comment>();

            var assignments = context.Assignments.ToList();
            var users = context.Users.ToList();

            for (int i = 1; i <= 10; i++)
            {
                var assignment = assignments[random.Next(assignments.Count)];
                var user = users[random.Next(users.Count)];

                comments.Add(new Comment
                {
                    AssignmentId = assignment.AssignmentId,
                    CreatedByUserId = user.UserId,
                    CreatedDate = DateTime.Now.AddDays(-random.Next(1, 10)),
                    CommentContent = $"This is comment number {i}"
                });
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();
        }

        static void SeedGrades(UniversityDbContext context)
        {
            if (context.Grades.Any())
                return;

            var random = new Random();
            var grades = new List<Grade>();

            var students = context.Users.Where(u => u.Role == "Student").ToList();
            var assignments = context.Assignments.ToList();

            foreach (var student in students)
            {
                foreach (var assignment in assignments)
                {
                    grades.Add(new Grade
                    {
                        StudentId = student.UserId,
                        AssignmentId = assignment.AssignmentId,
                        Score = random.Next(50, 101)
                    });
                }
            }

            context.Grades.AddRange(grades);
            context.SaveChanges();
        }
       


    }



}


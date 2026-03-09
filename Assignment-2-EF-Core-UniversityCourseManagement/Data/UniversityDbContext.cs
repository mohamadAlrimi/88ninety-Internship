using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.Data
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Syllabus> Syllabi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=UniversityCourseManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(64)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(64)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.EmailAddress)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasMaxLength(32)
                .IsRequired();

            // Course -> Teacher
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseId);

            modelBuilder.Entity<Course>()
                .Property(c => c.CourseName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(u => u.TaughtCourses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Assignment -> Course
            modelBuilder.Entity<Assignment>()
                .HasKey(a => a.AssignmentId);

            modelBuilder.Entity<Assignment>()
                .Property(a => a.AssignmentTitle)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId);

            // Comment -> Assignment
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Assignment)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AssignmentId);

            // Comment -> User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Grade -> Assignment
            modelBuilder.Entity<Grade>()
                .HasKey(g => g.GradeId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Assignment)
                .WithMany(a => a.Grades)
                .HasForeignKey(g => g.AssignmentId);

            // Grade -> Student
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Syllabus -> Course (One to One)
            modelBuilder.Entity<Syllabus>()
                .HasKey(s => s.SyllabusId);

            modelBuilder.Entity<Syllabus>()
                .HasOne(s => s.Course)
                .WithOne(c => c.Syllabus)
                .HasForeignKey<Syllabus>(s => s.CourseId);
        }
    }
}
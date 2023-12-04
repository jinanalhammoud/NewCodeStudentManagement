using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<ClassRegistration> ClassRegistrations { get; set; }
        public DbSet<ClassLecture> ClassLectures { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}

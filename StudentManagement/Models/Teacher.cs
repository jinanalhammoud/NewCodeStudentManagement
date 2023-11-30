using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class TeacherDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

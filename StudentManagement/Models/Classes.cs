using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Classes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }
    }

    public class ClassDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Course { get; set; }
        public string Teacher { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }
    }

}

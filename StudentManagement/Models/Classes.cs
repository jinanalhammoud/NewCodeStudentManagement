using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class Classes
    {
        public int Id { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
    }

    public class ClassDetails
    {
        public int Id { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        public string Course { get; set; }
        public string Teacher { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }
    }

    public class ClassInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

}

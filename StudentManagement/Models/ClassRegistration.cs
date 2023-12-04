using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class ClassRegistration
    {
        public int Id { get; set; }
        [Display(Name ="Student")]
        public int StudentId { get; set; }
        [Display(Name ="Class")]
        public int ClassId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        [ForeignKey(nameof(ClassId))]
        public Classes Class { get; set; }

    }

    public class ClassRegistrationDetails
    {
        public int Id { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
    }
}

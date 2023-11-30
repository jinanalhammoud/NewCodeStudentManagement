using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class ClassRegistration
    {
        public int Id { get; set; }
        [Display(Name ="Student")]
        public int StudentId { get; set; }
        [Display(Name ="Class")]
        public int ClassId { get; set; }

    }

    public class ClassRegistrationDetails
    {
        public int Id { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
    }
}

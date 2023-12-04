using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class ClassLecture
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "Class")]
        public int ClassId { get; set; }
        public DateTime Day { get; set; }
        [Display(Name ="Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name ="End Time")]
        public DateTime EndTime { get; set; }

        [ForeignKey(nameof(ClassId))]
        public Classes Class { get; set; }
    }

    public class ClassLectureDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public string Day { get; set; }
        [Display(Name ="Start Time")]
        public string StartTime { get; set; }
        [Display(Name ="End Time")]
        public string EndTime { get; set; }
    }
}

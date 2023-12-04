using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public int StudentId { get; set; }

        [ForeignKey(nameof(LectureId))]
        public ClassLecture Lecture { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
    }

    public class AttendanceDetails
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Lecture { get; set; }
        public string Student { get; set; }
    }

    public class SelectClassViewModel
    {
        [Display(Name = "Select a Class")]
        public int SelectedClassId { get; set; }

        public SelectList Classes { get; set; }
    }

    public class MarkAttendanceViewModel
    {
        [Display(Name = "Selected Class")]
        public int SelectedClassId { get; set; }

        [Display(Name = "Selected Lecture")]
        public int SelectedLectureId { get; set; }

        public List<StudentViewModel> Students { get; set; }
        [BindNever]
        public SelectList Lectures { get; set; }
    }

    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Mark Attendance")]
        public bool Attended { get; set; }
    }
}

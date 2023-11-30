namespace StudentManagement.Models
{
    public class ClassLecture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }
        public DateTime Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ClassLectureDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

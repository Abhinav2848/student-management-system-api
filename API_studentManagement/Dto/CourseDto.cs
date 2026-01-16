namespace API_studentManagement.Dto
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public int CourseDuration { get; set; }
        public decimal CourseFess { get; set; }
    }
}

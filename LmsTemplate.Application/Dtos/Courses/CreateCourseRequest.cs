namespace LmsTemplate.Application.Dtos.Courses
{
    public class CreateCourseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}

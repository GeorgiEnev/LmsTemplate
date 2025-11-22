using LmsTemplate.Application.Dtos.Courses;

namespace LmsTemplate.Application.Dtos.AcademicRoles
{
    public class AcademicRoleDetailsDto
    {
        public int Id { get; set; }
        public int Period { get; set; }
        public int Term { get; set; }
        public string Specialty { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public List<CourseDto> AssignedCourses { get; set; } = new();
    }
}

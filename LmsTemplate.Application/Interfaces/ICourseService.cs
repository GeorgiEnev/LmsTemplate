using LmsTemplate.Application.Dtos.Courses;

namespace LmsTemplate.Application.Interfaces
{
    public interface ICourseService
    {
        Task<int> CreateCourseAsync(CreateCourseRequest createCourseRequest);

        Task<IReadOnlyList<CourseDto>> GetAllCoursesAsync();

        Task<CourseDto?> GetByIdAsync(int id);
    }
}

using LmsTemplate.Application.Dtos.Courses;
using LmsTemplate.Application.Interfaces;
using LmsTemplate.Domain.Entities;
using LmsTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LmsTemplate.Infrastructure.Services
{
    public class CourseService:ICourseService
    {
        private readonly LmsDbContext _context;

        public CourseService(LmsDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCourseAsync(CreateCourseRequest createCourseRequest)
        {
            if (createCourseRequest == null)
                throw new ArgumentNullException(nameof(createCourseRequest));

            if (string.IsNullOrWhiteSpace(createCourseRequest.Title))
                throw new ArgumentException("Course title cannot be empty.");

            if (string.IsNullOrWhiteSpace(createCourseRequest.Code))
                throw new ArgumentException("Course code cannot be empty.");

            var duplicate = await _context.Courses
                .AnyAsync(c => c.Code == createCourseRequest.Code);

            if (duplicate)
                throw new ArgumentException("A course with the same code already exists.");

            var course = new Course
            {
                Title = createCourseRequest.Title,
                Code = createCourseRequest.Code,
                Description = createCourseRequest.Description,
                IsActive = true
            };

            _context.Courses.Add(course);

            await _context.SaveChangesAsync();

            return course.Id;
        }

        public Task<IReadOnlyList<CourseDto>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

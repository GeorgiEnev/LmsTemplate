using LmsTemplate.Application.Dtos.Courses;
using LmsTemplate.Application.Interfaces;
using LmsTemplate.Domain.Entities;
using LmsTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LmsTemplate.Infrastructure.Services
{
    public class CourseService : ICourseService
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

        public async Task<IReadOnlyList<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses
                .AsNoTracking()
                .ToListAsync();

            var dtoList = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Code = c.Code,
                Description = c.Description,
                IsActive = c.IsActive
            }).ToList();

            return dtoList;
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var     course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Description = course.Description,
                IsActive = course.IsActive
            };
        }

        public async Task<bool> UpdateCourseAsync(int id, UpdateCourseRequest request)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Course title cannot be empty.");

            if (string.IsNullOrWhiteSpace(request.Code))
                throw new ArgumentException("Course code cannot be empty.");

            bool duplicate = await _context.Courses
                .AnyAsync(c => c.Code == request.Code && c.Id != id);

            if (duplicate)
                throw new ArgumentException("Another course with this code already exists.");

            course.Title = request.Title;
            course.Code = request.Code;
            course.Description = request.Description;

            await _context.SaveChangesAsync();

            return true;
        }

    }
}

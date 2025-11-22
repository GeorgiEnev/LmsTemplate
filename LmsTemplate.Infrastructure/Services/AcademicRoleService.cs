using LmsTemplate.Application.Dtos.AcademicRoles;
using LmsTemplate.Application.Dtos.Courses;
using LmsTemplate.Application.Interfaces;
using LmsTemplate.Domain.Entities;
using LmsTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LmsTemplate.Infrastructure.Services
{
    public class AcademicRoleService : IAcademicRoleService
    {
        private readonly LmsDbContext _context;

        public AcademicRoleService(LmsDbContext context)
        {
            _context = context;
        }

        public async Task<AcademicRoleDto> CreateAsync(CreateAcademicRoleRequest request)
        {
            var existing = await _context.AcademicRoles
                .FirstOrDefaultAsync(r =>
                    r.Period == request.Period &&
                    r.Term == request.Term &&
                    r.Specialty == request.Specialty);

            if (existing != null)
            {
                return MapToDto(existing);
            }

            var role = new AcademicRole
            {
                Period = request.Period,
                Term = request.Term,
                Specialty = request.Specialty,
                IsActive = true
            };

            _context.AcademicRoles.Add(role);
            await _context.SaveChangesAsync();

            return MapToDto(role);
        }

        public async Task<IReadOnlyList<AcademicRoleDto>> GetAllAsync(bool includeInactive = false)
        {
            var query = _context.AcademicRoles.AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(r => r.IsActive);
            }

            var roles = await query
                .OrderBy(r => r.Period)
                .ThenBy(r => r.Term)
                .ThenBy(r => r.Specialty)
                .ToListAsync();

            return roles.Select(MapToDto).ToList();
        }

        public async Task<AcademicRoleDto?> GetByIdAsync(int id)
        {
            var role = await _context.AcademicRoles.FindAsync(id);
            return role == null ? null : MapToDto(role);
        }

        public async Task DeactivateAsync(int id)
        {
            var role = await _context.AcademicRoles.FindAsync(id);
            if (role == null)
            {
                return;
            }

            role.IsActive = false;
            await _context.SaveChangesAsync();
        }

        private static AcademicRoleDto MapToDto(AcademicRole role)
        {
            return new AcademicRoleDto
            {
                Id = role.Id,
                Period = role.Period,
                Term = role.Term,
                Specialty = role.Specialty,
                IsActive = role.IsActive
            };
        }

        public async Task<List<int>> GetAssignedCourseIdsAsync(int roleId)
        {
            var courseIds = await _context.AcademicRoleCourses
                .Where(arc => arc.AcademicRoleId == roleId)
                .Select(arc => arc.CourseId)
                .ToListAsync();

            return courseIds;
        }

        public async Task<AssignCoursesToRoleViewModel> BuildAssignCoursesViewModelAsync(int roleId)
        {
            var role = await _context.AcademicRoles.FindAsync(roleId);

            if (role == null)
            {
                throw new Exception("Academic role not found.");
            }

            var allCourses = await _context.Courses
                .OrderBy(c => c.Title)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Code = c.Code,
                    Description = c.Description,
                    IsActive = c.IsActive
                })
                .ToListAsync();

            var assignedCourseIds = await GetAssignedCourseIdsAsync(roleId);

            var vm = new AssignCoursesToRoleViewModel
            {
                AcademicRoleId = role.Id,
                AcademicRoleName = $"{role.Specialty} - Term {role.Term}, Period {role.Period}",
                AllCourses = allCourses,
                SelectedCourseIds = assignedCourseIds
            };

            return vm;
        }

        public async Task AssignCoursesAsync(int roleId, List<int> selectedCourseIds)
        {
            var existing =  await _context.AcademicRoleCourses
                .Where(arc => arc.AcademicRoleId == roleId)
                .ToListAsync();

            var existingCourseIds = existing
            .Select(e => e.CourseId)
            .ToList();

            var toAdd = selectedCourseIds
            .Except(existingCourseIds)
            .ToList();

            var toRemove = existingCourseIds
            .Except(selectedCourseIds)
            .ToList();

            foreach (var courseId in toAdd)
            {
                var newAssignment = new AcademicRoleCourse
                {
                    AcademicRoleId = roleId,
                    CourseId = courseId
                };

                _context.AcademicRoleCourses.Add(newAssignment);
            }

            foreach (var courseId in toRemove)
            {
                var assignment = existing
                    .FirstOrDefault(e => e.CourseId == courseId);

                if (assignment != null)
                {
                    _context.AcademicRoleCourses.Remove(assignment);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<AcademicRoleDetailsDto?> GetDetailsAsync(int id)
        {
            var role = await _context.AcademicRoles.FindAsync(id);
            if (role == null)
                return null;

            var assignedIds = await GetAssignedCourseIdsAsync(id);

            var courses = await _context.Courses
                .Where(c => assignedIds.Contains(c.Id))
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Code = c.Code,
                    Description = c.Description,
                    IsActive = c.IsActive
                })
                .ToListAsync();

            return new AcademicRoleDetailsDto
            {
                Id = role.Id,
                Period = role.Period,
                Term = role.Term,
                Specialty = role.Specialty,
                IsActive = role.IsActive,
                AssignedCourses = courses
            };
        }

    }
}

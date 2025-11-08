using LmsTemplate.Application.Dtos.AcademicRoles;
using LmsTemplate.Application.Interfaces;
using LmsTemplate.Domain.Entities;
using LmsTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LmsTemplate.Infrastructure.Services
{
    public class UserAcademicRoleService : IUserAcademicRoleService
    {
        private readonly LmsDbContext _context;

        public UserAcademicRoleService(LmsDbContext context)
        {
            _context = context;
        }

        public async Task AssignRoleToUserAsync(AssignUserAcademicRoleRequest request)
        {
            var role = await _context.AcademicRoles
                .FirstOrDefaultAsync(r => r.Id == request.AcademicRoleId && r.IsActive);

            if (role == null)
            {
                return;
            }

            var existingAssignment = await _context.UserAcademicRoles
                .FirstOrDefaultAsync(a =>
                    a.UserId == request.UserId &&
                    a.AcademicRoleId == request.AcademicRoleId &&
                    a.IsCurrent == request.IsCurrent);

            if (existingAssignment != null)
            {
                return;
            }

            if (request.IsCurrent)
            {
                var currentAssignments = await _context.UserAcademicRoles
                    .Where(a => a.UserId == request.UserId && a.IsCurrent)
                    .ToListAsync();

                foreach (var assignment in currentAssignments)
                {
                    assignment.IsCurrent = false;
                }
            }

            var newAssignment = new UserAcademicRole
            {
                UserId = request.UserId,
                AcademicRoleId = request.AcademicRoleId,
                AssignedAt = System.DateTime.UtcNow,
                AssignedByUserId = request.AssignedByUserId,
                IsCurrent = request.IsCurrent
            };

            _context.UserAcademicRoles.Add(newAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleFromUserAsync(int userAcademicRoleId)
        {
            var assignment = await _context.UserAcademicRoles
                .FirstOrDefaultAsync(a => a.Id == userAcademicRoleId);

            if (assignment == null)
            {
                return;
            }

            _context.UserAcademicRoles.Remove(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<AcademicRoleDto>> GetCurrentRolesForUserAsync(string userId)
        {
            var roles = await _context.UserAcademicRoles
                .Where(a => a.UserId == userId && a.IsCurrent)
                .Include(a => a.AcademicRole)
                .Select(a => a.AcademicRole)
                .ToListAsync();

            return roles
                .Select(r => new AcademicRoleDto
                {
                    Id = r.Id,
                    Period = r.Period,
                    Term = r.Term,
                    Specialty = r.Specialty,
                    IsActive = r.IsActive
                })
                .ToList();
        }
    }
}

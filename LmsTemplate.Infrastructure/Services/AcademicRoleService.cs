using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsTemplate.Application.Dtos.AcademicRoles;
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
    }
}

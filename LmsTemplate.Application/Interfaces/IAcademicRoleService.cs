using System.Collections.Generic;
using System.Threading.Tasks;
using LmsTemplate.Application.Dtos.AcademicRoles;

namespace LmsTemplate.Application.Interfaces
{
    public interface IAcademicRoleService
    {
        Task<AcademicRoleDto> CreateAsync(CreateAcademicRoleRequest request);

        Task<IReadOnlyList<AcademicRoleDto>> GetAllAsync(bool includeInactive = false);

        Task<AcademicRoleDto?> GetByIdAsync(int id);

        Task DeactivateAsync(int id);

        Task<List<int>> GetAssignedCourseIdsAsync(int roleId);

        Task AssignCoursesAsync(int roleId, List<int> selectedCourseIds);

        Task<AssignCoursesToRoleViewModel> BuildAssignCoursesViewModelAsync(int roleId);
    }
}

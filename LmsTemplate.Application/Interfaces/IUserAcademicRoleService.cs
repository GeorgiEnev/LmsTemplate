using System.Collections.Generic;
using System.Threading.Tasks;
using LmsTemplate.Application.Dtos.AcademicRoles;

namespace LmsTemplate.Application.Interfaces
{
    public interface IUserAcademicRoleService
    {
        Task AssignRoleToUserAsync(AssignUserAcademicRoleRequest request);

        Task RemoveRoleFromUserAsync(int userAcademicRoleId);

        Task<IReadOnlyList<AcademicRoleDto>> GetCurrentRolesForUserAsync(string userId);
    }
}

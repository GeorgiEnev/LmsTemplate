using System.Collections.Generic;
using LmsTemplate.Application.Dtos.AcademicRoles;
using LmsTemplate.Application.Dtos.Users;

namespace LmsTemplate.Web.Models
{
    public class UserDetailsViewModel
    {
        public UserSummaryDto User { get; set; } = null!;

        public IReadOnlyList<AcademicRoleDto> CurrentAcademicRoles { get; set; } = new List<AcademicRoleDto>();

        public IReadOnlyList<AcademicRoleDto> AvailableRoles { get; set; } = new List<AcademicRoleDto>();

        public AssignAcademicRoleInput AssignInput { get; set; } = new AssignAcademicRoleInput();
    }

    public class AssignAcademicRoleInput
    {
        public string UserId { get; set; } = string.Empty;

        public int SelectedAcademicRoleId { get; set; }
    }
}

namespace LmsTemplate.Application.Dtos.AcademicRoles
{
    public class AssignUserAcademicRoleRequest
    {
        public string UserId { get; set; } = string.Empty;

        public int AcademicRoleId { get; set; }

        public bool IsCurrent { get; set; } = true;

        public string AssignedByUserId { get; set; } = string.Empty;
    }
}

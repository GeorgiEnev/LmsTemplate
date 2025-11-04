namespace LmsTemplate.Application.Dtos.AcademicRoles
{
    public class AcademicRoleDto
    {
        public int Id { get; set; }

        public int Period { get; set; }

        public int Term { get; set; }

        public string Specialty { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public string DisplayName => $"P{Period} T{Term} - {Specialty}";
    }
}

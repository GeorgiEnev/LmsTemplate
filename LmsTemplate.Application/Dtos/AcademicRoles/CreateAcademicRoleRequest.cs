namespace LmsTemplate.Application.Dtos.AcademicRoles
{
    public class CreateAcademicRoleRequest
    {
        public int Period { get; set; }

        public int Term { get; set; }

        public string Specialty { get; set; } = string.Empty;
    }
}

namespace LmsTemplate.Application.Dtos.Users
{
    public class UserSummaryDto
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? DisplayName { get; set; }

        public string[] Roles { get; set; } = [];
    }
}

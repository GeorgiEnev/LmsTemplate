using Microsoft.AspNetCore.Identity;

namespace LmsTemplate.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
    }
}

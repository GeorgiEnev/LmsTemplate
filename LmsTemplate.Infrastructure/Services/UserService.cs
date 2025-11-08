using LmsTemplate.Application.Dtos.Users;
using LmsTemplate.Application.Interfaces;
using LmsTemplate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LmsTemplate.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IReadOnlyList<UserSummaryDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var result = new List<UserSummaryDto>(users.Count);

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserSummaryDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    DisplayName = user.DisplayName,
                    Roles = roles.ToArray()
                });
            }

            return result;
        }

        public async Task<UserSummaryDto?> GetByIdAsync(string userId)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserSummaryDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                DisplayName = user.DisplayName,
                Roles = roles.ToArray()
            };
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}

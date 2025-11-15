using System.Security.Claims;
using System.Threading.Tasks;
using LmsTemplate.Application.Interfaces;
using LmsTemplate.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsTemplate.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAcademicRoleService _academicRoleService;
        private readonly IUserAcademicRoleService _userAcademicRoleService;

        public UsersController(
            IUserService userService,
            IAcademicRoleService academicRoleService,
            IUserAcademicRoleService userAcademicRoleService)
        {
            _userService = userService;
            _academicRoleService = academicRoleService;
            _userAcademicRoleService = userAcademicRoleService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var availableRoles = await _academicRoleService.GetAllAsync(includeInactive: false);
            var currentAcademicRoles = await _userAcademicRoleService.GetCurrentRolesForUserAsync(id);

            var vm = new UserDetailsViewModel
            {
                User = user,
                AvailableRoles = availableRoles,
                CurrentAcademicRoles = currentAcademicRoles,
                AssignInput = new AssignAcademicRoleInput
                {
                    UserId = id
                }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignAcademicRole(AssignAcademicRoleInput input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = input.UserId });
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Forbid();
            }

            var request = new LmsTemplate.Application.Dtos.AcademicRoles.AssignUserAcademicRoleRequest
            {
                UserId = input.UserId,
                AcademicRoleId = input.SelectedAcademicRoleId,
                IsCurrent = true,
                AssignedByUserId = currentUserId
            };

            await _userAcademicRoleService.AssignRoleToUserAsync(request);

            return RedirectToAction(nameof(Details), new { id = input.UserId });
        }
    }
}

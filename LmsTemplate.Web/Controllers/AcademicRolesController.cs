using System.Threading.Tasks;
using LmsTemplate.Application.Dtos.AcademicRoles;
using LmsTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsTemplate.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AcademicRolesController : Controller
    {
        private readonly IAcademicRoleService _academicRoleService;

        public AcademicRolesController(IAcademicRoleService academicRoleService)
        {
            _academicRoleService = academicRoleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _academicRoleService.GetAllAsync(includeInactive: true);
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAcademicRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _academicRoleService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = await _academicRoleService.GetDetailsAsync(id);

            if (role == null)
                return NotFound();

            return View(role);
        }


        public async Task<IActionResult> AssignCourses(int id)
        {
            var vm = await _academicRoleService.BuildAssignCoursesViewModelAsync(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCourses(int id, AssignCoursesToRoleViewModel model)
        {
            await _academicRoleService.AssignCoursesAsync(id, model.SelectedCourseIds);
            return RedirectToAction("Details", new { id });
        }
    }
}

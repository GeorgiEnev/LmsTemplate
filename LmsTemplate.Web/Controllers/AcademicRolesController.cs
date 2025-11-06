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
    }
}

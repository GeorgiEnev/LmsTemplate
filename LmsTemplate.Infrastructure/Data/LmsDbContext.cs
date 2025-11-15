using LmsTemplate.Domain.Entities;
using LmsTemplate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LmsTemplate.Infrastructure.Data
{
    public class LmsDbContext : IdentityDbContext<ApplicationUser>
    {
        public LmsDbContext(DbContextOptions<LmsDbContext> options)
            : base(options)
        {
        }

        public DbSet<AcademicRole> AcademicRoles { get; set; } = null!;

        public DbSet<UserAcademicRole> UserAcademicRoles { get; set; } = null!;

        public DbSet<Course> Courses { get; set; } = null!;
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LmsTemplate.Domain.Entities
{
    public class AcademicRole
    {
        public int Id { get; set; }

        [Range(1, 20)]
        public int Period { get; set; }

        [Range(1, 10)]
        public int Term { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialty { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<UserAcademicRole> UserAssignments { get; set; } = new List<UserAcademicRole>();

        public string DisplayName => $"P{Period} T{Term} - {Specialty}";
    }
}

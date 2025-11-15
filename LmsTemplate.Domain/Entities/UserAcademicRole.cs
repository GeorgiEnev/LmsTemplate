using System.ComponentModel.DataAnnotations;

namespace LmsTemplate.Domain.Entities
{
    public class UserAcademicRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int AcademicRoleId { get; set; }

        [Required]
        public DateTime AssignedAt { get; set; }

        [Required]
        [MaxLength(450)]
        public string AssignedByUserId { get; set; } = string.Empty;

        public bool IsCurrent { get; set; } = true;

        public AcademicRole AcademicRole { get; set; } = null!;
    }
}

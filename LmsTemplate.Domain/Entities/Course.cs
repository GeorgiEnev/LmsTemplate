using System.ComponentModel.DataAnnotations;

namespace LmsTemplate.Domain.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]

        public string Code { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Description { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace LmsTemplate.Domain.Entities
{
    public class AcademicRoleCourse
    {
        [Key]
        public int Id { get; set; }

        public int AcademicRoleId { get; set; }
        public AcademicRole AcademicRole { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}

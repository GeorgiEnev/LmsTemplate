using LmsTemplate.Application.Dtos.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LmsTemplate.Application.Dtos.AcademicRoles
{
    public class AssignCoursesToRoleViewModel
    {
        public int AcademicRoleId { get; set; }
        public string AcademicRoleName { get; set; } = string.Empty;    

        public List<CourseDto> AllCourses { get; set; } = new ();

        public List<int> SelectedCourseIds { get; set; } = new ();  
    }
}

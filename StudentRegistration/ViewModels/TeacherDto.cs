using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistration.ViewModels
{
    public class TeacherDto
    {
        public int Tid { get; set; }
        public string TeacherName { get; set; }
        public Nullable<int> TeacherCnic { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public List<int?> SubjectIds { get; set; }
        public string SubjectNames { get; set; }
        public List<SubjectsDto> Subjects { get; set; }
    }
    public class SubjectsDto 
    {
        public string SubjectName { get; set; }
    }
}
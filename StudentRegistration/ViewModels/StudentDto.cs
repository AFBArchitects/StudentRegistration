
using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistration.ViewModels
{
    public class StudentDto
    {
        public int Sid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public Nullable<int> StudentCnic { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int SubAssignId { get; set; }
        public Nullable<int> SubjectID { get; set; }
        public List<int?> SubjectIds { get; set; }
        public string SubjectNames { get; set; }

        public List<SubjectDto> Subjects { get; set; }
    }

    public class SubjectDto
    {
        public string SubjectName { get; set; }
    }
}
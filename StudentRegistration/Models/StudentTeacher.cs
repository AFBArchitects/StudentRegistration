//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentRegistration.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentTeacher
    {
        public int StudentTeacherID { get; set; }
        public Nullable<int> TR_FK { get; set; }
        public Nullable<int> SR_FK { get; set; }
    
        public virtual StdRegistration StdRegistration { get; set; }
        public virtual TeacherRegistration TeacherRegistration { get; set; }
    }
}
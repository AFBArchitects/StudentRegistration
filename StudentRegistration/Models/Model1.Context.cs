﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SchoolSystemEntities : DbContext
    {
        public SchoolSystemEntities()
            : base("name=SchoolSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Class> Classes { get; set; }
        public DbSet<StdRegistration> StdRegistrations { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectAssign> SubjectAssigns { get; set; }
        public DbSet<TeacherAssign> TeacherAssigns { get; set; }
        public DbSet<TeacherRegistration> TeacherRegistrations { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}

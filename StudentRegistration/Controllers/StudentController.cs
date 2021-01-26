using StudentRegistration.Models;
using StudentRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistration.Controllers
{

    public class StudentController : Controller
    {
        SchoolSystemEntities ab = new SchoolSystemEntities();

        public ActionResult StudentRecord()
        {
            var std = new List<StudentDto>();

            std = ab.StdRegistrations.Select(a => new StudentDto
            {
                Sid = a.Sid,
                FirstName = a.FirstName,
                LastName = a.LastName,
                FatherName = a.FatherName,
                StudentCnic = a.StudentCnic,
                Address = a.Address,
                Email = a.Email,
                SubjectID = a.SubjectID,
                Subjects = (from sb in ab.SubjectAssigns
                            join s in ab.Subjects on sb.SS_FK equals s.SubjectID
                            where sb.SR_FK == a.Sid
                            select new SubjectDto
                            {
                                SubjectName = s.SubjectName,
                            }).ToList()

            }).ToList();
            std.ForEach(x =>
            {
                if (x.Subjects != null)
                {
                    x.SubjectNames = string.Join(", ", x.Subjects.Select(p => p.SubjectName.ToString()));
                }

            });
            ViewBag.List = std;
            return View();
        }
        [HttpGet]

        public ActionResult AddEditStudent(int id = 0)
        {
            var subjects = ab.Subjects.ToList();
            ViewBag.subjects = subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.SubjectName }).ToList();
            var std = new StudentDto();
            if (id > 0)
            {
                std = ab.StdRegistrations.Where(s => s.Sid == id).Select(a => new StudentDto
                {
                    Sid = a.Sid,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    FatherName = a.FatherName,
                    StudentCnic = a.StudentCnic,
                    Address = a.Address,
                    Email = a.Email,
                    SubjectID = a.SubjectID,
                    SubjectIds = ab.SubjectAssigns.Where(s => s.SR_FK == id).Select(c => c.SS_FK).ToList()

                }).FirstOrDefault();
            }

            return PartialView("_AddEditStudent", std);
        }
        [HttpPost]
        public ActionResult AddEditStudent(StudentDto model)
        {
            var item = ab.StdRegistrations.FirstOrDefault(x => x.Sid == model.Sid);
            if (item == null)
            {
                item = new StdRegistration();
                ab.StdRegistrations.Add(item);
            }
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.FatherName = model.FatherName;
            item.StudentCnic = model.StudentCnic;
            item.Address = model.Address;
            item.Email = model.Email;

            if (model.Password != null)
            {
                item.Password = model.Password;
            }

            ab.SaveChanges();

            var temp = item.Sid;
            if (model.SubjectIds.Count > 0)
            {
                var assignList = ab.SubjectAssigns.Where(x => x.SR_FK == model.Sid).ToList();
                if (assignList.Count > 0)
                {
                    ab.SubjectAssigns.RemoveRange(assignList);
                    ab.SaveChanges();
                }

                foreach (var item1 in model.SubjectIds)
                {
                    SubjectAssign subjectAssign = new SubjectAssign();
                    subjectAssign.SR_FK = temp;
                    subjectAssign.SS_FK = item1;
                    ab.SubjectAssigns.Add(subjectAssign);
                    ab.SaveChanges();
                }

                ab.SaveChanges();
            }
            return RedirectToAction("StudentRecord");
        }
        public ActionResult Delete(int id)
        {
            ab.StudentTeachers.RemoveRange(ab.StudentTeachers.Where(x => x.SR_FK == id));
            ab.SubjectAssigns.RemoveRange(ab.SubjectAssigns.Where(x => x.SR_FK == id));

            ab.StdRegistrations.Remove(ab.StdRegistrations.Where(x => x.Sid == id).First());
            ab.SaveChanges();
            return RedirectToAction("StudentRecord");
        }
        public ActionResult ProfileView(int id)
        {
            var std = new StudentDto();
            std = ab.StdRegistrations.Where(x => x.Sid == id).Select(a => new StudentDto
            {
                Sid = a.Sid,
                FirstName = a.FirstName,
                LastName = a.LastName,
                FatherName = a.FatherName,
                StudentCnic = a.StudentCnic,
                Address = a.Address,
                Email = a.Email,
                SubjectID = a.SubjectID,
                Subjects = (from sb in ab.SubjectAssigns
                            join s in ab.Subjects on sb.SS_FK equals s.SubjectID
                            where sb.SR_FK == a.Sid
                            select new SubjectDto
                            {
                                SubjectName = s.SubjectName,
                            }).ToList()
            }).FirstOrDefault();

            std.SubjectNames = string.Join(", ", std.Subjects.Select(p => p.SubjectName.ToString())); 

            return PartialView("_ProfileView", std);
        }
    }
}
using StudentRegistration.Models;
using StudentRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace StudentRegistration.Controllers
{
    public class TeacherController : Controller
    {
        SchoolSystemEntities ab = new SchoolSystemEntities();

        public ActionResult TeacherRecord() => View(LoadData());

        public ActionResult GetList() => PartialView("_List", LoadData());
        
        public List<TeacherDto> LoadData()
        {
            var teacherList = new List<TeacherDto>();
            teacherList = ab.TeacherRegistrations.Select(a => new TeacherDto
            {
                Tid = a.Tid,
                TeacherName = a.TeacherName,
                TeacherCnic = a.TeacherCnic,
                Address = a.Address,
                Email = a.Email,
                SubjectID = a.SubjectID,
                Subjects = (from sb in ab.TeacherAssigns
                            join s in ab.Subjects on sb.SS_FK equals s.SubjectID
                            where sb.TR_FK == a.Tid
                            select new SubjectsDto
                            {
                                SubjectName = s.SubjectName,
                            }).ToList()

            }).ToList();
            teacherList.ForEach(x =>
            {
                if (x.Subjects != null)
                {
                    x.SubjectNames = string.Join(", ", x.Subjects.Select(p => p.SubjectName.ToString()));
                }

            });
            return teacherList;
        }

        public ActionResult AddEditTeacher(int id = 0)
        {
            var subjects = ab.Subjects.ToList();
            ViewBag.subjects = subjects.Select(x => new SelectListItem { Value = x.SubjectID.ToString(), Text = x.SubjectName }).ToList();

            var tea = new TeacherDto();

            if (id > 0)
            {
                tea = ab.TeacherRegistrations.Where(a => a.Tid == id).Select(a => new TeacherDto
                {
                    Tid = a.Tid,
                    TeacherName = a.TeacherName,
                    TeacherCnic = a.TeacherCnic,
                    Address = a.Address,
                    Email = a.Email,
                    SubjectID = a.SubjectID,
                    SubjectIds = ab.TeacherAssigns.Where(s => s.TR_FK == id).Select(b => b.SS_FK).ToList()

                }).FirstOrDefault();
            }

            return PartialView("_AddEditTeacher", tea);
        }
        [HttpPost]
        public ActionResult AddEditTeacher(TeacherDto model)
        {
            var item = ab.TeacherRegistrations.FirstOrDefault(x => x.Tid == model.Tid);
            if (item == null)
            {
                item = new TeacherRegistration();
                ab.TeacherRegistrations.Add(item);
            }

            item.TeacherName = model.TeacherName;
            item.TeacherCnic = model.TeacherCnic;
            item.Address = model.Address;
            item.Email = model.Email;
            if (model.Password != null)
            {
                item.Password = model.Password;
            }
            ab.SaveChanges();

            var temp = item.Tid;
            if (model.SubjectIds.Count > 0)
            {
                var ListAssign = ab.TeacherAssigns.Where(x => x.TR_FK == model.Tid).ToList();
                if (ListAssign.Count > 0)
                {
                    ab.TeacherAssigns.RemoveRange(ListAssign);
                    ab.SaveChanges();
                }

                foreach (var item1 in model.SubjectIds)
                {
                    TeacherAssign teacherAssign = new TeacherAssign();
                    teacherAssign.TR_FK = temp;
                    teacherAssign.SS_FK = item1;
                    ab.TeacherAssigns.Add(teacherAssign);
                    ab.SaveChanges();
                }
                ab.SaveChanges();
            }

            return RedirectToAction("TeacherRecord");

        }


        public ActionResult Delete(int id)
        {

            ab.TeacherAssigns.RemoveRange(ab.TeacherAssigns.Where(x => x.TR_FK == id));
            ab.TeacherRegistrations.Remove(ab.TeacherRegistrations.Where(x => x.Tid == id).First());
            ab.SaveChanges();
            return RedirectToAction("TeacherRecord");

        }

        public ActionResult ProfileViewTeacher(int id)
        {
            var tea = new TeacherDto();
            tea = ab.TeacherRegistrations.Where(x => x.Tid == id).Select(a => new TeacherDto()
            {
                Tid = a.Tid,
                TeacherName = a.TeacherName,
                TeacherCnic = a.TeacherCnic,
                Address = a.Address,
                Email = a.Email,
                SubjectID = a.SubjectID,
                Subjects = (from sb in ab.TeacherAssigns
                            join s in ab.Subjects on sb.SS_FK equals s.SubjectID
                            where sb.TR_FK == a.Tid
                            select new SubjectsDto
                            {
                                SubjectName = s.SubjectName,
                            }).ToList()

            }).FirstOrDefault();

            tea.SubjectNames = string.Join(", ", tea.Subjects.Select(p => p.SubjectName.ToString()));

            return PartialView("_ProfileViewTeacher", tea);
        }
    }
}
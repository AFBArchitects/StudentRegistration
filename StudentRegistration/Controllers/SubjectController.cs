
using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistration.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        SchoolSystemEntities ab = new SchoolSystemEntities();

        [HttpGet]
        public ActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(Subject model)

        {
            Subject obj = new Subject();
            obj.SubjectName = model.SubjectName;
            ab.Subjects.Add(obj);
            ab.SaveChanges();
            return RedirectToAction("ListSubjects");

        }
        public ActionResult ListSubjects()
        {
            var item = ab.Subjects.ToList();

            ViewBag.List = item;
            return View();
        }
        public ActionResult Delete(int id)
        {

            var item = ab.Subjects.Where(x => x.SubjectID == id).First();
            ab.Subjects.Remove(item);
            ab.SaveChanges();
            return RedirectToAction("ListSubjects");
        }
        [HttpGet]

        public ActionResult EditSubject(int id)
        {
            var item = ab.Subjects.FirstOrDefault(x => x.SubjectID == id);
            return View("EditSubject", item);
        }
        [HttpPost]
        public ActionResult EditSubject(Subject model)
        {

            if (model.SubjectID > 0)
            {
                var item = ab.Subjects.FirstOrDefault(x => x.SubjectID == model.SubjectID);
                item.SubjectName = model.SubjectName;


                ab.SaveChanges();
                return RedirectToAction("ListSubjects");
            }
            return View();
        }
    }
}
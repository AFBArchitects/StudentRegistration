using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistration.Controllers
{
    public class TeacherController : Controller
    {
        SchoolSystemEntities ab = new SchoolSystemEntities();
        // GET: Teacher
        [HttpGet]
        public ActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(TeacherRegistration model)

        {
            TeacherRegistration obj = new TeacherRegistration();
            obj.TeacherName = model.TeacherName;
            obj.TeacherCnic = model.TeacherCnic;
            obj.Address = model.Address;
            obj.Email = model.Email;
            obj.Password = model.Password;
            ab.TeacherRegistrations.Add(obj);
            ab.SaveChanges();

            return RedirectToAction("TeacherRecord");

        }
        public ActionResult TeacherRecord()
        { 
            var item = ab.TeacherRegistrations.ToList();
       
            return View(item);
        }
        public ActionResult Delete(int id)
        {

            var item = ab.TeacherRegistrations.Where(x => x.Tid == id).First();
            ab.TeacherRegistrations.Remove(item);
            ab.SaveChanges();
            return RedirectToAction("TeacherRecord");
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {
            var item = ab.TeacherRegistrations.FirstOrDefault(x => x.Tid == id);
            return View("Edit", item);
        }
        [HttpPost]
        public ActionResult Edit(TeacherRegistration model)
        {

            if (model.Tid > 0)
            {
                var item = ab.TeacherRegistrations.FirstOrDefault(x => x.Tid == model.Tid);
                item.TeacherName= model.TeacherName;
                item.TeacherCnic = model.TeacherCnic;
                item.Address = model.Address;
                item.Email = model.Email;
                if (model.Password != null)
                {
                    item.Password = model.Password;
                }

                ab.SaveChanges();
                return RedirectToAction("TeacherRecord");
            }
            return View();
        }
        public ActionResult ProfileViewTeacher(int id)
        {

            var item = ab.TeacherRegistrations.Where(x => x.Tid == id).FirstOrDefault();
            ViewBag.item = item;

            return View();
        }
    }
}
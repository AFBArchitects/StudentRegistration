using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistration.Controllers
{
    public class ClassController : Controller
    {
        SchoolSystemEntities ab = new SchoolSystemEntities();
        // GET: Class
        [HttpGet]
        public ActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClass(Class model)

        {
            Class obj = new Class();
            obj.ClassName = model.ClassName;
            ab.Classes.Add(obj);
            ab.SaveChanges();
            return RedirectToAction("ClassRecord");

        }
        public ActionResult ClassRecord()
        {
            var item = ab.Classes.ToList();
            ViewBag.List = item;
            return View();
        }
        public ActionResult Delete(int id)
        {

            var item = ab.Classes.Where(x => x.ClassID == id).First();
            ab.Classes.Remove(item);
            ab.SaveChanges();
            return RedirectToAction("ClassRecord");
        }
        [HttpGet]

        public ActionResult EditClass(int id)
        {
            var item = ab.Classes.FirstOrDefault(x => x.ClassID == id);
            return View("EditClass", item);
        }
        [HttpPost]
        public ActionResult EditClass(Class model)
        {

            if (model.ClassID > 0)
            {
                var item = ab.Classes.FirstOrDefault(x => x.ClassID == model.ClassID);
                item.ClassName = model.ClassName;
                

                ab.SaveChanges();
                return RedirectToAction("ClassRecord");
            }
            return View();
        }
    }
}
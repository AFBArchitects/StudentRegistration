using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistration.Controllers
{
    public class LoginController : Controller
    {
        SchoolSystemEntities ab = new SchoolSystemEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(StdRegistration abc)
        {
            var result = ab.StdRegistrations.Where(x => x.Email == abc.Email && x.Password == abc.Password).FirstOrDefault();
            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(StdRegistration model)
        {
            StdRegistration obj = new StdRegistration();
            obj.FirstName = model.FirstName;
            obj.LastName = model.LastName;
            obj.FatherName = model.FatherName;
            obj.StudentCnic = model.StudentCnic;
            obj.Address = model.Address;
            obj.Email = model.Email;
            obj.Password = model.Password;
            ab.StdRegistrations.Add(obj);
            ab.SaveChanges();

            return RedirectToAction("Login");       
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            System.Web.HttpContext.Current.Response.Redirect("/Login/Login");
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universityty;
using Universityty.Models;
using Registryry;

namespace Universityty.Controllers
{
    public class HomeController : Controller
    {

        static Dbase ado = Dbase.instance;


        [HttpGet]
        public ViewResult Index()
        {

            return View();
        }

        public ViewResult ViewSchedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Normal m)
        {
            string e = m.Email;
            string p = m.Pword;
            TempData["tempstudent"] = m;
            m = ado.MatchStudentLogin(m.Email, m.Pword);
            if (m.Email == e && m.Pword == p)
            {
                return RedirectToAction("ShowStudentSchedule", "Home");
            }
            else
                return View();

        }


        public ViewResult ShowStudentSchedule(Normal n)
        {
            //use a method to iterate through all the student's current courses taken

            return View(TempData["tempstudent"]);
        }

        public PartialViewResult ShowStudentSchedule()
        {
            return PartialViewResult()
        }
    }
}

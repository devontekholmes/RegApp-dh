using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universityty.Models;

namespace Universityty.Controllers
{
    public class StudentController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

       /* public ActionResult GetEveryStudent()
        {
            var data = Dbase.instance;
            ModelState.Clear();
            return View(data.GetAllStudents());
        }
        */

        public ActionResult AddStudent()
        {
            return View();
        }
        
        [HttpPost] 
        public ActionResult AddStudent(Normal D) 
        {
            var data = Dbase.instance;
            try
            {
                if (ModelState.IsValid)
                {
                    if (data.AddStudent(D))
                    {
                        ViewBag.Message = "Student added.";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]

        public ActionResult UpdateStudentDetails(int id, Normal D)
        {
            var data = Dbase.instance;
            try
            {
                data.UpdateStudent(D);
                return RedirectToAction("GetEveryStudent");
            }
            catch
            {
                return View();
            }

        }

        public ActionResult DeleteStudent(int id)
        {
            var data = Dbase.instance;
            try
            {
                if (data.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student info deleted";
                }
                return RedirectToAction("GetEveryStudent");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universityty.Models;
using Registryry;

namespace Universityty.Controllers
{
    public class StudentController : Controller
    {
        static Dbase ado = Dbase.instance;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCourse()
        {
            return View(Dbase.GetAllCourses());
        }

        [HttpPost]
        public ActionResult AddCourse(Course c)
        {
            Course addedCourse = new Course();
            addedCourse = Dbase.GetCourseInfo(c.CourseId);
            return PartialView(Dbase.AddCourses(, c));
        }
        

        public ActionResult AddStudent()
        {
            List<Course> n = new List<Course>();
            n = Dbase.currentStudents.Schedule;
           return View();
        }
        
        public ActionResult DropCourse()
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
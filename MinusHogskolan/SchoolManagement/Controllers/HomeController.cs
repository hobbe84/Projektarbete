using SchoolManagement.DataAccess;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: GetAllCourses
        [HttpGet]
        public ActionResult GetAllCourses()
        {
            List<Courses> AllCourses;

            // Skapa koppling till databasen
            using(var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram alla kurser ur tabellen Courses och sortera på namn.
                AllCourses = ctx.Courses
                    .Where(x => x.ActiveCourse == true)
                    .Select(x => new Courses
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Info = x.Info,
                        Points = x.Points,
                        ActiveCourse = x.ActiveCourse
                    }).OrderBy(x => x.Name).ToList();
            }

            return View(AllCourses);
        }
        [HttpGet]
        public ActionResult GetAllStudents()
        {
            List<Students> AllStudents;

            using (var ctx = new MinushogskolanDbEntities())
            {
                AllStudents = ctx.Students
                    .Where(x => x.ActiveStudent == true)
                    .Select(x => new Students
                    {
                        ID = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Address = x.Adress,
                        City = x.City,
                        ActiveStudent = x.ActiveStudent
                    }).OrderBy(x => x.FirstName + x.LastName).ToList();
            }
            return View(AllStudents);
        }
        [HttpGet]
        public ActionResult GetAllTeachers()
        {
            List<Teachers> AllTeachers;

            using (var ctx = new MinushogskolanDbEntities())
            {
                AllTeachers = ctx.Teachers
                    .Where(x => x.ActiveTeacher == true)
                    .Select(x => new Teachers
                    {
                        ID = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Address = x.Adress,
                        City = x.City,
                        ActiveTeacher = x.ActiveTeacher
                    }).OrderBy(x => x.FirstName + x.LastName).ToList();
            }
            return View(AllTeachers);
        }
        [HttpGet]
        public ActionResult DeleteCourse(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Courses course;
            using (var ctx = new MinushogskolanDbEntities())
            {
                var query =
                    (from c in ctx.Courses
                     where c.ID == id
                     select new Courses
                     {
                         Name = c.Name,
                         Info = c.Info,
                         Points = c.Points
                     });
                course = query.FirstOrDefault();                 
            }
            if (course == null)
            {
                return HttpNotFound();
            } 
            return View(course);
        }
        [HttpPost]
        [ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                using (var ctx = new MinushogskolanDbEntities())
                {
                    var query = ctx.Courses
                        .FirstOrDefault(c => c.ID == id);
                        

                    query.ActiveCourse = false;
                    ctx.SaveChanges();
                }
            }
            catch 
            {
                return RedirectToAction("DeleteCourse", new { id = id });
            }
            return RedirectToAction("GetAllCourses");
        }
        [HttpGet]
        public ActionResult DeleteStudent(int? id)
        {
            return View();
        }
        [HttpPost]
        [ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                using (var ctx = new MinushogskolanDbEntities())
                {
                    var query = ctx.Students
                        .FirstOrDefault(u => u.ID == id);


                    query.ActiveStudent = false;
                    ctx.SaveChanges();
                }
            }
            catch
            {
                return RedirectToAction("DeleteStudent", new { id = id });
            }
            return RedirectToAction("GetAllStudents");
        }
        [HttpGet]
        public ActionResult DeleteTeacher(int? id)
        {
            return View();
        }
        [HttpPost]
        [ActionName("DeleteTeacher")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTeacher(int id)
        {
            try
            {
                using (var ctx = new MinushogskolanDbEntities())
                {
                    var query = ctx.Teachers
                        .FirstOrDefault(c => c.ID == id);


                    query.ActiveTeacher = false;
                    ctx.SaveChanges();
                }
            }
            catch
            {
                return RedirectToAction("DeleteTeacher", new { id = id });
            }
            return RedirectToAction("GetAllTeachers");
        }




        
    }

    
}
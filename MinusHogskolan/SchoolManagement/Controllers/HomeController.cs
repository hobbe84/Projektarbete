using SchoolManagement.DataAccess;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    .Select(x => new Courses
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Info = x.Info,
                        Points = x.Points
                    }).OrderBy(x => x.Name).ToList();
            }

            return View(AllCourses);
        }
    }

    
}
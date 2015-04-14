using SchoolManagement.DataAccess;
using SchoolManagement.Models;
using SchoolManagement.ViewModels;
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
        

        // GET: Alla kurser
        [HttpGet]
        public ActionResult GetAllCourses()
        {
            // Deklarera en lista med kurser.
            List<CourseViewModel> AllCourses;

            // Skapa koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram alla kurser ur tabellen Courses som inte är avregistrerade och sortera på namn.
                AllCourses = ctx.Courses
                    .Where(x => x.ActiveCourse == true)
                    .Select(x => new CourseViewModel
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Info = x.Info,
                        Points = x.Points,
                        ActiveCourse = x.ActiveCourse
                    }).OrderBy(x => x.Name).ToList();
            }

            // Returnera vyn med alla kurser.
            return View(AllCourses);
        }

        // GET: Skapa ny kurs
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }


        //POST: Skapa kursen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse([Bind(Include = "Name, Info, Points")]CourseViewModel addCourse)
        {
            // Kolla att formuläret är korrekt ifyllt och att det gick att spara i databasen annars ge felmeddelande till användaren.
            if (ModelState.IsValid)
            {
                try
                {
                    // Skapa en koppling till databasen.
                    using (var ctx = new MinushogskolanDbEntities())
                    {
                        var course = new Course
                        {
                            Name = addCourse.Name,
                            Info = addCourse.Info,
                            Points = addCourse.Points
                        };

                        // Sätt kursen som aktiv för att kunna visa den i lista med kurser.
                        course.ActiveCourse = true;

                        ctx.Courses.Add(course);
                        ctx.SaveChanges();

                        // Feedback till användaren att det gick att registrera kursen.
                        TempData["success"] = "Kursen har registrerats";
                    }
                }
                catch
                {
                    // Felmeddelande till användaren.
                    TempData["error"] = "Misslyckades med att registrera kursen, försök igen";
                }
            }
            // Gå tillbaka till listan med kurser.
            return RedirectToAction("GetAllCourses");
        }

        // GET: Uppdatera en kurs
        [HttpGet]
        public ActionResult EditCourse(int? id)
        {
            // Om id är tom/null så finns inte kursen.
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseViewModel courseToEdit;
            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                courseToEdit = ctx.Courses
                    .Where(x => x.ID == id)
                    .Select(x => new CourseViewModel
                    {
                        Name = x.Name,
                        Info = x.Info,
                        Points = x.Points
                    }).FirstOrDefault();
            }

            // Kursen hittades inte.
            if (courseToEdit == null)
            {
                return HttpNotFound();
            }

            // Returnera vyn med kursen.
            return View(courseToEdit);
        }


        // POST: Uppdatera kursen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(CourseViewModel courseToEdit)
        {
            try
            {
                // Skapa en koppling till databasen.
                using (var ctx = new MinushogskolanDbEntities())
                {
                    // Ta fram kursen från databasen som motsvarar id:t på kursen som ska uppdateras.
                    var changedCourse = ctx.Courses
                        .Where(x => x.ID == courseToEdit.ID)
                        .FirstOrDefault();

                    // Om kursen fanns, uppdatera fälten i formuläret med de nya inmatade parametrarna.
                    if (courseToEdit != null)
                    {
                        changedCourse.Name = courseToEdit.Name;
                        changedCourse.Info = courseToEdit.Info;
                        changedCourse.Points = courseToEdit.Points;
                    }

                    // Spara ändringarna.
                    ctx.SaveChanges();

                    // Feedback till användaren att det gick att registrera kursen.
                    TempData["success"] = "Kursen har uppdaterats";
                }
            }
            catch
            {
                // Felmeddelande till användaren.
                TempData["error"] = "Misslyckades med att uppdatera kursen, försök igen";
            }

            // Gå tillbaka till listan med kurser.
            return RedirectToAction("GetAllCourses");
        }


        // GET: Avregistrera en kurs
        [HttpGet]
        public ActionResult DeleteCourse(int? id)
        {
            // Om id är tom/null så finns inte kursen.
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Deklarera variabel.
            CourseViewModel course;

            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram kursen med det medskickade id:t för att visa vilken kurs som ska avregistreras.
                course = ctx.Courses
                    .Where(x => x.ID == id)
                    .Select(x => new CourseViewModel
                    {
                        Name = x.Name,
                        Info = x.Info,
                        Points = x.Points
                    }).FirstOrDefault();
            }

            // Kursen hittades inte.
            if (course == null)
            {
                return HttpNotFound();
            }

            // Returnera vyn med kursen.
            return View(course);
        }


        // POST: Avregistrera kursen
        [HttpPost]
        [ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(int id)
        {
            // Kolla om kommandona till databasen går rätt till annars felhantera.
            try
            {
                // Skapa en koppling till databasen.
                using (var ctx = new MinushogskolanDbEntities())
                {
                    // Ta fram kursen som ska avregistreras, sätt den som ej aktiv kurs samt spara ändringen i databasen.
                    var query = ctx.Courses
                        .FirstOrDefault(c => c.ID == id);

                    query.ActiveCourse = false;
                    ctx.SaveChanges();
                }
                TempData["success"] = string.Format("Kursen har avregistrerats");
            }
            catch
            {
                TempData["error"] = "Misslyckades att avregistrera kursen. Försök igen!";
                return RedirectToAction("DeleteCourse", new { id = id });
            }

            // Gå tillbaka till listan med kurser.
            return RedirectToAction("GetAllCourses");
        }


        // GET: Alla studenter
        [HttpGet]
        public ActionResult GetAllStudents()
        {
            // Deklarera en lista med student-objekt.
            List<StudentViewModel> AllStudents;

            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram alla studenter ur tabellen Students som inte är avregistrerade och sortera på för- och efternamn.
                AllStudents = ctx.Students
                    .Where(x => x.ActiveStudent == true)
                    .Select(x => new StudentViewModel
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

            // Returnera vyn med alla studenter.
            return View(AllStudents);
        }


        // GET: Skapa ny student
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }


        //POST: Skapa studenten
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent([Bind(Include = "FirstName, LastName, BirthDate, Address, City")]StudentViewModel addStudent)
        {
            // Kolla att formuläret är korrekt ifyllt och att det gick att spara i databasen annars ge felmeddelande till användaren.
            if (ModelState.IsValid)
            {
                try
                {
                    // Skapa en koppling till databasen.
                    using (var ctx = new MinushogskolanDbEntities())
                    {
                        var student = new Student
                        {
                            FirstName = addStudent.FirstName,
                            LastName = addStudent.LastName,
                            BirthDate = addStudent.BirthDate,
                            Adress = addStudent.Address,
                            City = addStudent.City

                        };

                        // Sätt student som aktiv för att kunna visa den i lista med studenter.
                        student.ActiveStudent = true;

                        ctx.Students.Add(student);
                        ctx.SaveChanges();

                        // Feedback till användaren att det gick att registrera studenten.
                        TempData["success"] = "Studenten har registrerats";
                    }
                }
                catch
                {
                    // Felmeddelande till användaren.
                    TempData["error"] = "Misslyckades med att registrera studenten, försök igen";
                }
            }
            // Gå tillbaka till listan med studenter.
            return RedirectToAction("GetAllStudents");
        }


        // GET: Uppdatera en student
        [HttpGet]
        public ActionResult EditStudent(int? id)
        {
            // Om id är tom/null så finns inte studenten.
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StudentViewModel studentToEdit;

            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                studentToEdit = ctx.Students
                    .Where(x => x.ID == id)
                    .Select(x => new StudentViewModel
                    {
                        ID = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Address = x.Adress,
                        City = x.City
                    }).FirstOrDefault();
            }

            // Studenten hittades inte.
            if (studentToEdit == null)
            {
                return HttpNotFound();
            }

            // Returnera vyn med Studenter.
            return View(studentToEdit);
        }


        // POST: Uppdatera student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent([Bind(Include = "ID, FirstName, LastName, BirthDate, Address, City")]StudentViewModel studentToEdit)
        {
            try
            {
                // Skapa en koppling till databasen.
                using (var ctx = new MinushogskolanDbEntities())
                {
                    // Ta fram studenten från databasen som motsvarar id:t på student som ska uppdateras.
                    var changedStudent = ctx.Students
                        .Where(x => x.ID == studentToEdit.ID)
                        .FirstOrDefault();

                    // Om studenten fanns, uppdatera fälten i formuläret med det nya inmatade parametrarna.
                    if (studentToEdit != null)
                    {
                        changedStudent.FirstName = studentToEdit.FirstName;
                        changedStudent.LastName = studentToEdit.LastName;
                        changedStudent.BirthDate = studentToEdit.BirthDate;
                        changedStudent.Adress = studentToEdit.Address;
                        changedStudent.City = studentToEdit.City;
                    }

                    // Spara ändringarna.
                    ctx.SaveChanges();

                    // Feedback till användaren att det gick att registrera kursen.
                    TempData["success"] = "Studenten har uppdaterats";
                }
            }
            catch
            {
                // Felmeddelande till användaren.
                TempData["error"] = "Misslyckades med att uppdatera studenten, försök igen";
            }

            // Gå tillbaka till listan med studenter.
            return RedirectToAction("GetAllStudents");
        }


        // GET: Avregistrera en student
        [HttpGet]
        public ActionResult DeleteStudent(int? id)
        {
            // Om id är tom/null så hanteras felet.
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Deklarera variabel.
            StudentViewModel student;

            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram studenten med det medskickade id:t för att visa vilken student som ska avregistreras.
                student = ctx.Students
                    .Where(x => x.ID == id)
                    .Select(x => new StudentViewModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Address = x.Adress,
                        City = x.City
                    }).FirstOrDefault();
            }

            // Studenten hittades inte.
            if (student == null)
            {
                return HttpNotFound();
            }

            // Returnera vyn med studenten.
            return View(student);
        }


        // POST: Avregistrera studenten
        [HttpPost]
        [ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                // Skapa en koppling till databasen.
                using (var ctx = new MinushogskolanDbEntities())
                {
                    // Ta fram studenten som ska avregistreras, sätt den som ej aktiv student samt spara ändringen i databasen.
                    var query = ctx.Students
                        .FirstOrDefault(u => u.ID == id);

                    query.ActiveStudent = false;
                    ctx.SaveChanges();
                }
                TempData["success"] = string.Format("Studenten är avregistrerad");
            }
            catch
            {
                TempData["error"] = "Misslyckades att avregistrera studenten. Försök igen!";
                return RedirectToAction("DeleteStudent", new { id = id });
            }

            // Gå tillbaka till llistan med studenter.
            return RedirectToAction("GetAllStudents");
        }


        // GET: Alla lärare
        [HttpGet]
        public ActionResult GetAllTeachers()
        {
            // Deklarera en lista med lärar-objekt.
            List<TeacherViewModel> AllTeachers;

            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram alla lärare ur tabellen Teachers som inte är avregistrerade och sortera på för- och efternamn.
                AllTeachers = ctx.Teachers
                    .Where(x => x.ActiveTeacher == true)
                    .Select(x => new TeacherViewModel
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

            // Returnera vyn med alla lärare.
            return View(AllTeachers);
        }
        // GET: Skapa ny lärare
        [HttpGet]
        public ActionResult AddTeacher()
        {
            return View();
        }


        //POST: Skapa läraren
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeacher([Bind(Include = "FirstName, LastName, BirthDate, Address, City")]TeacherViewModel addTeacher)
        {
            // Kolla att formuläret är korrekt ifyllt och att det gick att spara i databasen annars ge felmeddelande till användaren.
            if (ModelState.IsValid)
            {
                try
                {
                    // Skapa en koppling till databasen.
                    using (var ctx = new MinushogskolanDbEntities())
                    {
                        var teacher = new Teacher
                        {
                            FirstName = addTeacher.FirstName,
                            LastName = addTeacher.LastName,
                            BirthDate = addTeacher.BirthDate,
                            Adress = addTeacher.Address,
                            City = addTeacher.City
                        };

                        // Sätt student som aktiv för att kunna visa den i lista med lärare.
                        teacher.ActiveTeacher = true;

                        ctx.Teachers.Add(teacher);
                        ctx.SaveChanges();

                        // Feedback till användaren att det gick att registrera läraren.
                        TempData["success"] = "Läraren har registrerats";
                    }
                }
                catch
                {
                    // Felmeddelande till användaren.
                    TempData["error"] = "Misslyckades med att registrera läraren, försök igen";
                }
            }
            // Gå tillbaka till listan med lärare.
            return RedirectToAction("GetAllTeachers");
        }


        // GET: Uppdatera en lärare
        [HttpGet]
        public ActionResult EditTeacher(int? id)
        {
            // Om id är tom/null så finns inte läraren.
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TeacherViewModel teacherToEdit;

            using (var ctx = new MinushogskolanDbEntities())
            {
                teacherToEdit = ctx.Teachers
                    .Where(x => x.ID == id)
                    .Select(x => new TeacherViewModel
                    {
                        ID = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Address = x.Adress,
                        City = x.City
                    }).FirstOrDefault();
            }

            // Läraren hittades inte.
            if (teacherToEdit == null)
            {
                return HttpNotFound();
            }

            // Returnera vyn med lärare.
            return View(teacherToEdit);
        }


        // POST: Uppdatera lärare
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeacher([Bind(Include = "FirstName, LastName, BirthDate, Address, City")]TeacherViewModel teacher)
        {
            try
            {
                // Skapa en koppling till databasen.
                using (var ctx = new MinushogskolanDbEntities())
                {
                    // Ta fram läraren från databasen som motsvarar id:t på lärare som ska uppdateras.
                    var teacherToEdit = ctx.Teachers
                        .Where(x => x.ID == teacher.ID)
                        .FirstOrDefault();

                    // Om läraren fanns, uppdatera fälten i formuläret med det nya inmatade parametrarna.
                    if (teacherToEdit != null)
                    {
                        teacherToEdit.FirstName = teacher.FirstName;
                        teacherToEdit.LastName = teacher.LastName;
                        teacherToEdit.BirthDate = teacher.BirthDate;
                        teacherToEdit.Adress = teacher.Address;
                        teacherToEdit.City = teacher.City;
                    }

                    // Spara ändringarna.
                    ctx.SaveChanges();

                    // Feedback till användaren att det gick att registrera läraren.
                    TempData["success"] = "Läraren har uppdaterats";
                }
            }
            catch
            {
                // Felmeddelande till användaren.
                TempData["error"] = "Misslyckades med att uppdatera läraren, försök igen";
            }

            // Gå tillbaka till listan med lärare.
            return RedirectToAction("GetAllTeachers");
        }


        // GET: Avregistrera en lärare
        [HttpGet]
        public ActionResult DeleteTeacher(int? id)
        {
            // Om id är tom/null så finns hanteras felet.
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Deklarera variabel.
            TeacherViewModel teacher;

            // Skapa en koppling till databasen.
            using (var ctx = new MinushogskolanDbEntities())
            {
                // Ta fram läraren med det medskickade id:t för att visa vilken lärare som ska avregistreras.
                teacher = ctx.Teachers
                    .Where(x => x.ID == id)
                    .Select(x => new TeacherViewModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirthDate = x.BirthDate,
                        Address = x.Adress,
                        City = x.City
                    }).FirstOrDefault();
            }

            // Läraren hittades inte.
            if (teacher == null)
            {
                return HttpNotFound();
            }

            // Returnera vyn med läraren.
            return View(teacher);
        }


        // POST: Avregistrera läraren
        [HttpPost]
        [ActionName("DeleteTeacher")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTeacher(int id)
        {
            try
            {
                // Skapa en koppling till databasen.
                using (var ctx = new MinushogskolanDbEntities())
                {
                    // Ta fram läraren som ska avregistreras, sätt den som ej aktiv lärare samt spara ändringen i databasen.
                    var query = ctx.Teachers
                        .FirstOrDefault(c => c.ID == id);

                    query.ActiveTeacher = false;
                    ctx.SaveChanges();
                }
                TempData["success"] = string.Format("Läraren är avregistrerad");
            }
            catch
            {
                TempData["error"] = "Misslyckades att avregistrera läraren. Försök igen!";
                return RedirectToAction("DeleteTeacher", new { id = id });
            }

            // Gå tillbaka till listan med lärare.
            return RedirectToAction("GetAllTeachers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseSearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                IList<CourseViewModel> cvm;
                using (var ctx = new MinushogskolanDbEntities())
                {

                    cvm = (from c in ctx.Courses
                           where c.Name.Contains(search)
                           select new CourseViewModel { Name = c.Name, Info = c.Info, Points = c.Points }).ToList();

                    ViewBag.VisaKnapp = true;
                    return View("GetAllCourses", cvm);
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentSearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                IList<StudentViewModel> svm;
                using (var ctx = new MinushogskolanDbEntities())
                {

                    svm = (from s in ctx.Students
                           where s.FirstName.Contains(search) || s.LastName.Contains(search)
                           select new StudentViewModel { FirstName = s.FirstName, LastName = s.LastName, Address = s.Adress, BirthDate = s.BirthDate, City = s.City }).ToList();

                    ViewBag.VisaKnapp = true;
                    return View("GetAllStudents", svm);
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TeacherSearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                IList<TeacherViewModel> tvm;
                using (var ctx = new MinushogskolanDbEntities())
                {

                    tvm = (from t in ctx.Teachers
                           where t.FirstName.Contains(search) || t.LastName.Contains(search)
                           select new TeacherViewModel { FirstName = t.FirstName, LastName = t.LastName, Address = t.Adress, BirthDate = t.BirthDate, City = t.City }).ToList();

                    ViewBag.VisaKnapp = true;
                    return View("GetAllTeachers", tvm);
                }
            }
            return View("Index");
        }
    }
}
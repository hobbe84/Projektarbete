using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Courses
    {
        public int ID { get; set; }       
        public string Name { get; set; }
        public string Info { get; set; }
        public int Points { get; set; }
        public bool ActiveCourse { get; set; }
    }
}
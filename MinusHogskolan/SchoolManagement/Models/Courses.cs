using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Courses
    {
        public int ID { get; set; } 

        [DisplayName("Namn")]
        public string Name { get; set; }

        [DisplayName("Beskrivning")]
        public string Info { get; set; }

        [DisplayName("YH Poäng")]
        public int Points { get; set; }

        public bool ActiveCourse { get; set; }
    }
}
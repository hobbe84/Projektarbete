using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.ViewModels
{
    public class CourseViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Kursens namn måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Namnet måste innehålla minst 2 tecken"))]
        [DisplayName("Kursnamn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Beskrivning måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Beskrivningen måste innehålla minst 2 tecken"))]
        [DisplayName("Beskrivning")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Antal YH Poäng måste anges")]
        [Range(100, 600, ErrorMessage = "Antal poäng för {0} måste vara mellan {1} och {2}.")]
        [DisplayName("YH Poäng(5P / vecka)")]
        public int Points { get; set; }

        public bool ActiveCourse { get; set; }
    }
}
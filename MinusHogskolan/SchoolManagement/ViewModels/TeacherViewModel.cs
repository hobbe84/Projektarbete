using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.ViewModels
{
    public class TeacherViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Förnamn måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Förnamnet måste innehålla minst 2 tecken"))]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Efternamnet måste innehålla minst 2 tecken"))]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Födelsedatum måste anges")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DisplayName("Födelsedatum")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Adress måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Adress måste innehålla minst 2 tecken"))]
        [DisplayName("Adress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Stad måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Namnet måste innehålla minst 2 tecken"))]
        [DisplayName("Stad")]
        public string City { get; set; }

        public bool ActiveTeacher { get; set; }
    }
}
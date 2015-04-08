using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Students
    {
        public int ID { get; set; }
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }
        [DisplayName("Efternamn")]
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString="{0:d}")]
        [DisplayName("Födelsedatum")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Adress")]
        public string Address { get; set; }
        [DisplayName("Stad")]
        public string City { get; set; }
        public bool ActiveStudent { get; set; }
    }
}
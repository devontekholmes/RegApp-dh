using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Registryry;

namespace Universityty.Models
{
    public class Normal
    {
        public Normal()
        {
        }

        public Normal(int studentId, string FName, string LName, string email, string pword, List<Course> schedule)
        {
            StudentId = studentId;
            fName = FName;
            lName = LName;
            Email = email;
            Pword = pword;
            Schedule = schedule;
        }


        [Display(Name = "StudentId")]
        public int StudentId { get; set; }

       
        public string fName
        { get; set; }

        public string lName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email {get; set; }

        [Required(ErrorMessage = "Pword is required.")]
        public string Pword
        { get; set; }

        public List<Course> Schedule { get; set; } 

        }
}
    

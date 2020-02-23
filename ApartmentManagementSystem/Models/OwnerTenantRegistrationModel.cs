using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class OwnerTenantRegistrationModel
    {
        [Required(ErrorMessage ="Fname is required field ")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "Lname is required field ")]
        public string Lname { get; set; }
        [Required(ErrorMessage = " Age is required field ")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "please select the gender ")]
        public string Gender { get; set; }
        [Required(ErrorMessage = " Email is required field ")]
        [EmailAddress(ErrorMessage = "not in format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required field ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Cell number required field ")]
        public string Cell { get; set; }
        [Required(ErrorMessage = "please select the Maritalstatus ")]
        public string Marital_status { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
        public string Office_address { get; set; }
        public string On_date { get; set; }
        [Required(ErrorMessage = "please select the right block ")]
        public string Block { get; set; }
        [Required(ErrorMessage = "Enter the flat number ")]
        public int? Flat_no { get; set; }
        [Required(ErrorMessage = "please select the member type ")]
        public string Member_type { get; set; }
        public List<string> lstblock = new List<string>();
        public int Id { get; set; }
        [Required(ErrorMessage = "please select the desigination ")]
        public string Desigination { get; set; }
        public List<string> lstdesigination = new List<string>();
    }
}
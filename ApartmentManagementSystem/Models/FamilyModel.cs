using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class FamilyModel
    {
        [Required(ErrorMessage ="Name is Required")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        public string Cell { get; set; }
        [Required(ErrorMessage = "Relation is Required")]
        public string Relation { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public List<string> lstrelation = new List<string>();
        public string Email { get; set; }
        public string Block { get; set; }
        public int Flat_no { get; set; }
        [Required(ErrorMessage = "please select the complain type")]
        public string ComplainType { get; set; }
        [Required(ErrorMessage = "ComplainDescription is Required")]
        public string ComplainDescription { get; set; }
        public List<string> lstcomplaintype = new List<string>();
        public string Service_Provider { get; set; }
        public string RequestDate { get; set; }
        public string Member_Name { get; set; }
        public List<string> lstamenities = new List<string>();
        public string Booking_for { get; set; }
        public string On_date { get; set; }
        public string On_time { get; set; }
        public List<string> lsttimes = new List<string>();
        public string Status { get; set; }
        public List<string> lststatus = new List<string>();
    }
}
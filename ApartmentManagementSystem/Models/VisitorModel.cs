using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class VisitorModel
    {
        [Required(ErrorMessage ="name is mandatory")]
        public string VisitorName { get; set; }
        [Required (ErrorMessage ="cell number is required")]
        public string Visitor_Cellno { get; set; }
        [Required (ErrorMessage ="address is required")]
        public string Address { get; set; }
        [Required (ErrorMessage ="please select the gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="membername is required")]
        public string MemberName { get; set; }
        [Required(ErrorMessage ="please select the block")]
        public string Block { get; set; }
        public List<string> lstblock = new List<string>();
        [Required(ErrorMessage ="flat number is required")]
        public int? Flatno { get; set; }
        [Required(ErrorMessage ="age is required")]
        public int? Age { get; set; }
        [Required(ErrorMessage ="please select the relation")]
        public string Relation { get; set; }
        public List<string> lstrelation = new List<string>();
        //public bool In_Time { get; set; }
        //public bool Out_Time { get; set; }
        public string In_Time { get; set; }
        public string Out_Time { get; set; }
        public int Visitor_Id { get; set; }
    }
}
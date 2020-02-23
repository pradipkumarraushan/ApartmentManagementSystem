using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApartmentManagementSystem.Models
{
    public class MeetingModel
    {
        public string Message { get; set; }
        public string Location { get; set; }
        public string On_Date { get; set; }
        public string Send_To { get; set; }
        public string MeetingTime { get; set; }
        public List<string> lstsend_to = new List<string>();
        public List<string> lstlocation = new List<string>();
    }
}
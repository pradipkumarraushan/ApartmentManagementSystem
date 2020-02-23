using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmentManagementSystem.Models;
using ApartmentManagementSystem.ModelManager;
using System.Net;
using System.Collections.Specialized;

namespace ApartmentManagementSystem.Controllers
{
    [Authorize(Roles =Role.ASSOC_MEMBER)]
    public class AssociationMemberController : Controller
    {
        MeetingModelManger MeetingModel = new MeetingModelManger();
        MeetingModel OModel = new MeetingModel();
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MeetingRequest()
        {
            OModel.lstsend_to=MeetingModel.GetSendToList();
            OModel.lstlocation = MeetingModel.GetMeetingLocation();
            return View(OModel);
        }

        [HttpPost]
        public ActionResult MeetingRequest(MeetingModel model)
        {
            List<string> lstnumber=MeetingModel.GetNumber(model.Send_To);
            string format = model.Message+"/nLocation:"+model.Location+"/nDate:"+model.On_Date+"/nMeetingTime:"+model.MeetingTime;
            String message = HttpUtility.UrlEncode(format);
            for(int i=0;i<lstnumber.Count;i++)
            {
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "yourapiKey"},
                {"numbers" , lstnumber[i]},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                    string result = System.Text.Encoding.UTF8.GetString(response);
                    
                }
            }
            OModel.lstsend_to = MeetingModel.GetSendToList();
            OModel.lstlocation = MeetingModel.GetMeetingLocation();
            return View(OModel);
        }
    }
}
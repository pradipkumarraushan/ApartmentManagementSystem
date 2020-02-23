using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmentManagementSystem.Models;

namespace ApartmentManagementSystem.Controllers
{
    [Authorize(Roles = Role.OWNER+","+Role.ASSOC_MEMBER+","+Role.TENANT)]
    [HandleError(ExceptionType =typeof(NullReferenceException),View = "Home")]
    public class OwnerTenantController : Controller
    {
        FamilyModelManger ModelManger = new FamilyModelManger();
        CombineModel omodel = new CombineModel();
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddFamilyMember()
        {
            
            LoginModel user = (LoginModel)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Login", "HomePage");
            }
            omodel.lstmodel= ModelManger.GetFamilyMember(user.Email);
            Session["familymember"] = omodel.lstmodel;
            TempData["count"] = omodel.lstmodel.Count;
            omodel.GetModel.lstrelation =HttpContext.Application["lstRelations"] as List<string>;
            omodel.GetModel.Email = user.Email;
            return View(omodel);
        }

        [HttpPost]
        public ActionResult AddFamilyMember(CombineModel model,string operation )
        {
            int count = (int)TempData["count"];
            if(operation=="Add"&&count<=6)
            {
              ModelManger.CreateFamilyMember(model.GetModel);
              return RedirectToAction("AddFamilyMember");
            }
            omodel.GetModel.lstrelation =HttpContext.Application["lstRelations"] as List<string>;
            omodel.lstmodel = (List<FamilyModel>)Session["familymember"];
            return View(omodel);
        }

        public ActionResult DeleteFamilyMember(int id)
        {
           ModelManger.DeleteFamilyMember(id);
           return RedirectToAction("AddFamilyMember");
        }
        
        [HttpGet]
        public ActionResult AddRegularVisitor()
        {
            LoginModel user = (LoginModel)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Login", "HomePage");
            }
            omodel.lstmodel = ModelManger.GetRegularVisitor(user.Email);
            TempData["count"] = omodel.lstmodel.Count;
            omodel.GetModel.lstrelation = HttpContext.Application["lstRelations"] as List<string>;
            omodel.GetModel.Email = user.Email;
            return View(omodel);
        }

        [HttpPost]
        public ActionResult AddRegularVisitor(CombineModel model, string operation)
        {
            int count = (int)TempData["count"];
            if (operation == "Add" && count <= 6)
            {
                ModelManger.CreateRegularVisitor(model.GetModel);
            }
            return RedirectToAction("AddFamilyMember");
        }

        public ActionResult DeleteRegularVisitor(int id)
        {
            ModelManger.DeleteRegularVisitor(id);
            return RedirectToAction("AddFamilyMember");
        }

        [HttpGet]
        public ActionResult ComplainServiceRequest()
        {
            omodel.GetModel.lstcomplaintype = HttpContext.Application["lstcomplaints"] as List<string>;
            LoginModel user = (LoginModel)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Login", "HomePage");
            }
            DataSet ds = ModelManger.GetComplaintlist(user.Email);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            foreach(DataRow dr in dt.Rows)
            {
                omodel.GetModel.Name =dr.ItemArray[0].ToString();
                omodel.GetModel.Cell = dr.ItemArray[1].ToString();
                omodel.GetModel.Block = dr.ItemArray[2].ToString();
                omodel.GetModel.Flat_no = Convert.ToInt32(dr.ItemArray[3]);
            }
            dt = ds.Tables[1];
            foreach(DataRow dr in dt.Rows)
            {
                FamilyModel fm = new FamilyModel();
                fm.Id = Convert.ToInt32(dr.ItemArray[0]);
                fm.Name = dr.ItemArray[1].ToString();
                fm.Cell = dr.ItemArray[2].ToString();
                fm.Block = dr.ItemArray[3].ToString();
                fm.Flat_no = Convert.ToInt32(dr.ItemArray[4]);
                fm.ComplainType = dr.ItemArray[5].ToString();
                fm.ComplainDescription = dr.ItemArray[6].ToString();
                fm.RequestDate = dr.ItemArray[7].ToString();
                fm.Service_Provider = dr.ItemArray[8].ToString();
                omodel.lstmodel.Add(fm);
            }
            return View(omodel);
        }

        [HttpPost]
        public ActionResult ComplainServiceRequest(CombineModel request,string operation)
        {
            if(operation=="Send Request")
            {
                DateTime on_date = Request.RequestContext.HttpContext.Timestamp;
                request.GetModel.RequestDate = on_date.ToString();
                bool request_sent = ModelManger.RegisterComplaint(request.GetModel);
                if(request_sent)
                {
                    return RedirectToAction("ComplainServiceRequest");
                }
            }
            return View();
        }

        public ActionResult DeleteServiceRequest(int id)
        {
            ModelManger.DeleteServiceRequest(id); 
            return RedirectToAction("ComplainServiceRequest");
        }

        [HttpGet]
        public ActionResult BookAmenities()
        {
            omodel.GetModel.lstamenities = HttpContext.Application["amenitielst"] as List<string>;
            LoginModel user = (LoginModel)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Login", "HomePage");
            }
            DataSet ds = ModelManger.GetBookedDetail(user.Email);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                omodel.GetModel.Name = dr.ItemArray[0].ToString();
                omodel.GetModel.Cell = dr.ItemArray[1].ToString();
                omodel.GetModel.Block = dr.ItemArray[2].ToString();
                omodel.GetModel.Flat_no = Convert.ToInt32(dr.ItemArray[3]);
            }
            dt = ds.Tables[1];
            foreach (DataRow dr in dt.Rows)
            {
                FamilyModel fm = new FamilyModel();
                fm.Id = Convert.ToInt32(dr.ItemArray[0]);
                fm.Name = dr.ItemArray[1].ToString();
                fm.Cell = dr.ItemArray[2].ToString();
                fm.Block = dr.ItemArray[3].ToString();
                fm.Flat_no = Convert.ToInt32(dr.ItemArray[4]);
                fm.Booking_for = dr.ItemArray[5].ToString();
                fm.On_date = dr.ItemArray[6].ToString();
                fm.On_time = dr.ItemArray[7].ToString();
                omodel.lstmodel.Add(fm);
            }
            return View(omodel);
        }

        [HttpPost]
        public ActionResult BookAmenities(CombineModel bookamenitie)
        {
            bool request_sent = ModelManger.RegisterComplaint(bookamenitie.GetModel);
            return RedirectToAction("BookAmenities");
        }

        public ActionResult Timing(string book_for)
        {
            omodel.GetModel.lsttimes = ModelManger.GetTiming(book_for);
            return Json(omodel.GetModel.lsttimes);
        }
    }
}
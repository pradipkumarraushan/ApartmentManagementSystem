using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmentManagementSystem.Models;
using PagedList;

namespace ApartmentManagementSystem.Controllers
{
    [Authorize(Roles = Role.ASSOC_MANAGER)]
    public class AssociationManagerController : Controller
    {
        OwnerTenantRegistrationModelManger RegistrationModelManger = new OwnerTenantRegistrationModelManger();
        OwnerTenantRegistrationModel registrationModel =new OwnerTenantRegistrationModel();
        List<FamilyModel> lstfamilymodel = new List<FamilyModel>();
        CombineModel ocombinemodel = new CombineModel();

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "HomePage");
        }

        [HttpGet]
        public ActionResult AddOwnerTenant()
        {
            registrationModel.lstblock = HttpContext.Application["lstblock"] as List<string>;
            return View(registrationModel);
        }

        [HttpPost]
        public ActionResult AddOwnerTenant(OwnerTenantRegistrationModel model,string operation)
        {
            bool is_success = false;
            if (operation=="Create")
            {
                DateTime on_date = Request.RequestContext.HttpContext.Timestamp.Date;
                model.On_date = on_date.ToShortDateString();
                 is_success= RegistrationModelManger.CreateOwnerTenant(model);
            }
            if(is_success)
            {
                return RedirectToAction("GetAllOwnerTenant");
            }
            return RedirectToAction("AddOwnerTenant");
        }

        public ActionResult GetAllOwnerTenant()
        {
            List<OwnerTenantRegistrationModel> list = RegistrationModelManger.GetAllOwnerTenant();
            Session["ownertenant"] = list;
            return View(list);
        }

        [HttpPost]
        public ActionResult EditOwnerTenant(OwnerTenantRegistrationModel editdata,string operation)
        {
            if(operation=="Update")
            {
                bool is_updated = RegistrationModelManger.UpdateOwnerTenant(editdata);
                if (is_updated)
                {
                    return RedirectToAction("GetAllOwnerTenant");
                }
            }
            else
            {
                return RedirectToAction("GetAllOwnerTenant");
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult EditOwnerTenant(int id)
        {
            List<OwnerTenantRegistrationModel> list = (List<OwnerTenantRegistrationModel>) Session["ownertenant"];
             
            foreach (OwnerTenantRegistrationModel model in list)
            {
                if(id==model.Id)
                {
                    registrationModel = model;
                }
              
            }
            registrationModel.lstblock = HttpContext.Application["lstblock"] as List<string>;
            return View(registrationModel);
        }
        [HttpGet]
        public ActionResult DeleteOwnerTenant(int id)
        {
            bool is_deleted = RegistrationModelManger.DeleteOwnerTenant(id);
            if (is_deleted)
            {
                return RedirectToAction("GetAllOwnerTenant");
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddAssociationMember()
        {
            registrationModel  = RegistrationModelManger.GetDesigination();
            Session["desigination"] = registrationModel.lstdesigination;
            return View(registrationModel);
        }

        public ActionResult AddAssociationMember(string operation,OwnerTenantRegistrationModel model)
        {
            if (operation=="Search")
            {
                registrationModel = RegistrationModelManger.SearchMember(model);
                registrationModel.lstdesigination =(List<string>)Session["desigination"];
                ModelState.Clear();
                return View(registrationModel);
            }
            else if(operation=="Add")
            {
                bool is_success = RegistrationModelManger.AddAssociationMember(model);
                if(is_success)
                {
                    return RedirectToAction("DisplayAssociationMember");
                }
            }
            registrationModel.lstdesigination = (List<string>)Session["desigination"];
            ModelState.Clear();
            return View(registrationModel);
        }
        [HttpGet]
        public ActionResult DisplayAssociationMember()
        {
            List<OwnerTenantRegistrationModel> lst= RegistrationModelManger.GetAllAssociationMember();
            return View(lst);
        }

        [HttpPost]
        public ActionResult DisplayAssociationMember(int id)
        {
            RegistrationModelManger.RemoveAssociationMember(id);
            return View();
        }

        public ActionResult GetServiceRequest()
        {
            lstfamilymodel = RegistrationModelManger.GetComplaints();
            Session["lstcomplaint"] = lstfamilymodel;
            return View(lstfamilymodel);
        }

        [HttpGet]
        public ActionResult EditServiceRequest(int id)
        {
            lstfamilymodel = Session["lstcomplaint"] as List<FamilyModel>;
            FamilyModel model = new FamilyModel();
            foreach(FamilyModel fm in lstfamilymodel)
            {
                if(fm.Id==id)
                {
                    model = fm;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditServiceRequest(FamilyModel editrequest)
        {
            bool is_edited = RegistrationModelManger.EditRequest(editrequest);
            if(is_edited)
            {
                return RedirectToAction("GetServiceRequest");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ManageAmenities(int? page)
        {
            if(page==null)
            {
                lstfamilymodel = RegistrationModelManger.GetBookedAmenities();
                Session["BookedAmenities"] = lstfamilymodel;
                ocombinemodel.familypagedlst = lstfamilymodel.ToPagedList(page ?? 1, 1);
                return View(ocombinemodel);
            }
            else
            {
                lstfamilymodel = Session["BookedAmenities"] as List<FamilyModel>;
                ocombinemodel.familypagedlst = lstfamilymodel.ToPagedList(page ?? 1, 1);
                return View(ocombinemodel);
            }
        }

        [HttpPost]
        public ActionResult ManageAmenities(CombineModel search)
        {
            int? page = null;
            lstfamilymodel = Session["BookedAmenities"] as List<FamilyModel>;
            List<FamilyModel> lst = new List<FamilyModel>();
            foreach (FamilyModel fm in lstfamilymodel)
            {
                if (search.Search_bydate == fm.On_date )
                {
                    lst.Add(fm);
                }
            }
            Session["BookedAmenities"] = lst;
            ocombinemodel.familypagedlst = lst.ToPagedList(page ?? 1, 1);
            return View(ocombinemodel);
        }

        [HttpGet]
        public ActionResult EditBookedAmenitie(int id)
        {
            lstfamilymodel = Session["BookedAmenities"] as List<FamilyModel>;
            FamilyModel model = new FamilyModel();
            foreach(FamilyModel fm in lstfamilymodel)
            {
                if (id ==fm.Id)
                {
                    model = fm;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBookedAmenitie(FamilyModel editbooking)
        {
            bool is_edited = RegistrationModelManger.EditRequest(editbooking);
            if (is_edited)
            {
                return RedirectToAction("ManageAmenities");
            }
            return View();
        }

        public ActionResult DeleteBookedAmenitie(int id)
        {
            RegistrationModelManger.DeleteBooking(id);
            return RedirectToAction("ManageAmenities");
        }
        
    }
}
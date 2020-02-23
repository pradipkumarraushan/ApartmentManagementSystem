using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmentManagementSystem.Models;
using ApartmentManagementSystem.ModelManager;
using PagedList;

namespace ApartmentManagementSystem.Controllers
{
    
    public class SecurityController : Controller
    {
        public VisitorModel ovisitormodel = new VisitorModel();
        public VisitorModelManager omodelmanager = new VisitorModelManager();
        public PagedListModel opagedList = new PagedListModel();
        public List<VisitorModel> olstmodel = new List<VisitorModel>();

        [Authorize(Roles = Role.SECURITY)]
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNewVisitor()
        {
            ovisitormodel.lstblock = HttpContext.Application["lstblock"] as List<string>;
            ovisitormodel.lstrelation = HttpContext.Application["lstrelations"] as List<string>;
            return View(ovisitormodel);
        }

        [HttpPost]
        public ActionResult AddNewVisitor(VisitorModel model,string operation)
        {
            model.lstblock = HttpContext.Application["lstblock"] as List<string>;
            model.lstrelation = HttpContext.Application["lstrelations"] as List<string>;
            if (operation=="In")
            {
                DateTime intime= Request.RequestContext.HttpContext.Timestamp;
                model.In_Time = intime.ToString();
                omodelmanager.VisitorIn(model);
                ModelState.Clear();
                return View(model);
            }
            else if(operation=="Search")
            {
                ovisitormodel = omodelmanager.GetVisitor(model);
                ovisitormodel.lstblock = HttpContext.Application["lstblock"] as List<string>;
                ovisitormodel.lstrelation = HttpContext.Application["lstrelations"] as List<string>;
                ModelState.Clear();
                return View( ovisitormodel);
            }
            else if(operation=="Out")
            {
                DateTime outtime = Request.RequestContext.HttpContext.Timestamp;
                model.Out_Time = outtime.ToString();
                omodelmanager.VisitorOut(model);
                ModelState.Clear();
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult SearchRegularVisitor()
        {
            opagedList.lstblock = HttpContext.Application["lstblock"] as List<string>;
            opagedList.lstmodel = omodelmanager.GetAllRegularVisitor();
            Session["regularvisitor"] = opagedList.lstmodel;
            return View(opagedList);
        }

        [HttpPost]
        public ActionResult SearchRegularVisitor(string block, int flat_no)
        {
            opagedList.lstmodel = Session["regularvisitor"] as List<FamilyModel>;
            var data = from lst in opagedList.lstmodel where lst.Block == block && lst.Flat_no == flat_no select lst;
            return Json(data);
        }

        [HttpGet]
        [Authorize(Roles =Role.SECURITY+","+Role.ASSOC_MEMBER)]
        public ActionResult VisitorHistory(int? page)
        {
            opagedList.lstblock = HttpContext.Application["lstblock"] as List<string>; 
            if(page==null)
            {
                olstmodel = HttpContext.Application["visitorlst"] as List<VisitorModel>;
                Session["visitorlist"] = olstmodel;
                opagedList.pagedlst = olstmodel.ToPagedList(page ?? 1, 1);
                return View(opagedList);
            }
            else
            {
                olstmodel = (List<VisitorModel>)Session["visitorlist"];
                opagedList.pagedlst = olstmodel.ToPagedList(page ?? 1, 1);
                return View(opagedList);
            }
            
        }

        [HttpPost]
        public ActionResult VisitorHistory(PagedListModel search )
        {
            int? page = null;
            olstmodel = HttpContext.Application["visitorlst"] as List<VisitorModel>;
            List<VisitorModel> lst = new List<VisitorModel>();
            foreach (VisitorModel vm in olstmodel)
            {
                if(search.Block==vm.Block&&search.Flat_no==vm.Flatno)
                {
                    lst.Add(vm);
                }
            }
            Session["visitorlist"] = lst;
            opagedList.pagedlst = lst.ToPagedList(page ?? 1, 1);
            opagedList.lstblock = HttpContext.Application["lstblock"] as List<string>;
            return View(opagedList);
        }
    }
}
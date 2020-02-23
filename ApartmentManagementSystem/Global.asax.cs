using ApartmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ApartmentManagementSystem.ModelManager;

namespace ApartmentManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FamilyModelManger ModelManger = new FamilyModelManger();
            List<string> lstRelations=ModelManger.GetRelation();
            Application["lstRelations"] = lstRelations;
            List<string> lstcomplaints = ModelManger.GetComplain();
            Application["lstcomplaints"] = lstcomplaints;
            OwnerTenantRegistrationModelManger model = new OwnerTenantRegistrationModelManger();
            List<string> lstblock = model.GetBlockList();
            Application["lstblock"] = lstblock;
            VisitorModelManager omodelmanager = new VisitorModelManager();
            List<VisitorModel> visitorlst=omodelmanager.GetAllVisitor();
            Application["visitorlst"] = visitorlst;
            List<string> lstamenitie = ModelManger.GetAmenities();
            Application["amenitielst"] = lstamenitie;
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userdata = ticket.UserData;
                        string[] roles = userdata.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                    }
                }
            }
        }
       
    }
}

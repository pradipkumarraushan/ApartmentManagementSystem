using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ApartmentManagementSystem.Models;

namespace ApartmentManagementSystem.Controllers
{
    public class HomePageController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            LoginModelManger getrole = new LoginModelManger();
            string email = login.Email;
            LoginModel loginstatus=getrole.Login(login);
            loginstatus.Email = email;
            if (loginstatus.role == null)
            {
                loginstatus.Message = "invalid username and/or password";
                ModelState.Clear();
                return View("Login", loginstatus);
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, login.Email, DateTime.Now, DateTime.Now.AddMinutes(20), true, loginstatus.role, FormsAuthentication.FormsCookiePath);
                string hashcookies = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashcookies)
                {
                    Expires = ticket.Expiration
                };
                Response.Cookies.Add(cookie);
                string previouspageurl = Request.UrlReferrer.ToString();
                Session["user"] = loginstatus;
                if(loginstatus.role.Contains(Role.ASSOC_MANAGER))
                {
                    return RedirectToAction("Home", "AssociationManager");
                }
                else if(loginstatus.role.Contains(Role.ASSOC_HEAD)|| loginstatus.role.Contains(Role.ASSOC_MEMBER))
                {
                    return RedirectToAction("Home", "AssociationMember");
                }
                else if (loginstatus.role.Contains(Role.SECURITY))
                {
                    return RedirectToAction("Home", "Security");
                }
                else if (loginstatus.role.Contains(Role.OWNER)||loginstatus.role.Contains(Role.BLOCK_INCHARGE)|| loginstatus.role.Contains(Role.TENANT))
                {
                    return RedirectToAction("Home", "OwnerTenant");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(LoginModel model)
        {
            LoginModelManger getpassword = new LoginModelManger();
            LoginModel omodel=getpassword.GetPassword(model);
            if(omodel.Password!=null)
            {
                var senderEmail = new MailAddress("senderemail@gmail.com");
                var receiverEmail = new MailAddress(omodel.Email);
                var password = "Your Email Password here";
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(senderEmail.Address, password);
                    using (MailMessage mailmsg = new MailMessage(senderEmail, receiverEmail))
                    {
                        mailmsg.Subject = "";
                        mailmsg.Body = "";
                        client.Send(mailmsg);
                    } 
                }
                omodel.Message = "Your Password Has Been Sent To Your Email";
                return View();
            }
            else
            {
                omodel.Message = "Invalid Email Or Phone Number Please Enter Valid Email And Phone Number ";
                return View(omodel);
            }
            
        }
      
    }
}
﻿using StoreFrontApplication.UI.MVC.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;


namespace StoreFrontApplication.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            //build the message
            string message = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";
            //MailMessage 
            MailMessage mm = new MailMessage("admin@don-richardson.com", "donr82@sbcglobal.net", cvm.Subject, message);

            //MailMessage properties
            //Allow HTML in the email
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            //Respond to the sender, and not our website
            mm.ReplyToList.Add(cvm.Email);

            //SmtpClient - This is the info from the host that allows this to be sent
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client Credentials
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"],ToString(),ConfigurationManager.AppSettings["EmailPass"].ToString());

            //Try to send the email
            try
            {
                //attempt to send
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be completed at this time. Please try again later. Error Message:{ex.Message}<br/>{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }
}

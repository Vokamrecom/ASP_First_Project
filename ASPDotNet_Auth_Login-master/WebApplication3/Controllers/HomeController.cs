using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _application;
        [HttpGet]
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult TriggerUser(string id)
        {
            try
            {
                var user = _application.Users.FirstOrDefault(f => f.Email == id);
                if (user.LockoutEnabled)
                {
                    user.LockoutEnabled = false;
                }
                else
                {
                    user.LockoutEnabled = true;
                }
                _application.SaveChanges();
                return RedirectToAction("About");
            }
            catch (Exception e)
            {
                return RedirectToAction("About");
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                var user = _application.Users.Find(id);
                _application.Users.Remove(user);
                _application.SaveChanges();
                return RedirectToAction("About");
            }
            catch
            {
                return RedirectToAction("About");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Name, Email = model.Email };
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        public ActionResult About(ApplicationDbContext application)
        {
            _application = application;

            return View(_application.Users);
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult  Contact(string textBoxStringData, int textBoxIntData, string checkboxData)
        {
            //Do something
            SendEmail(this, null);
            ViewBag.Message = "Email submitted.";

            return View();
        }

        protected void SendEmail(object sender, EventArgs e)
        {
            string txtEmail = "ermakovkiril@gmail.com";
            string txtPassword = "enterpassword";
            string txtSubject = "Test email";
            string txtBody = "This is just a test email";
            string txtTo = "ermakovkiril@gmail.com";
            using (MailMessage mm = new MailMessage(txtEmail, txtTo))
            {
                mm.Subject = txtSubject;
                mm.Body = txtBody;
                //if (fuAttachment.HasFile)
                //{
                //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                //}
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
            }
        }
    }
}
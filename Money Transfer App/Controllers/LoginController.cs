using Lab_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Lab_Exam.Controllers
{
    public class LoginController : Controller
    {
        MyExamDBEntities db = new MyExamDBEntities();
        // GET: Login
        public ActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Cred cred)
        {
            List<AccountInfo> info = db.AccountInfoes.ToList();
            List<AccountInfo> accounts = (from abc in info
                                          where abc.Email.Equals(cred.Email)
                                          && abc.Password.Equals(cred.Password)
                                          select abc).ToList();
            if (accounts.Count > 0)
            {
                ViewBag.AccountInfo = accounts[0];
                Session["username"] = accounts[0].Email;
                ViewBag.name = accounts[0].FirstName;
                FormsAuthentication.SetAuthCookie(accounts[0].FirstName, false);
                return Redirect("/Account/Index");

            }
            else
            {
                ViewBag.message = "User Name or Password Wrong";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("Signin");
        }
    }
    public class Cred
    {
        public String Email { get; set; }
        public string Password { get; set; }
    }
}
using Lab_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_Exam.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        MyExamDBEntities db = new MyExamDBEntities();

        // GET: Account

        public ActionResult Index()
        {
            //var pass = Session["username"].ToString();
            //List<AccountInfo> accounts = (from abc in db.AccountInfoes
            //                              where 
            //                               abc.Email.Equals(pass)
            //                              select abc).ToList();
            //return View(accounts[0]);
            return View(db.AccountInfoes.ToList());
        }
        [HttpGet]
        public ActionResult Transaction( int id)
        {
            ViewBag.Accounts = db.AccountInfoes.ToList();
            return View(db.AccountInfoes.Find(id));
        }
        [HttpPost]
        public ActionResult Transaction(AccountInfo1 ac)
        {
            if (ac.Balance > ac.Amount)
            {
                if(ac.Account == ac.Account_No)
                {
                    ViewBag.MyMessage = "Please Choose different Acc No";
                    ViewBag.Accounts = db.AccountInfoes.ToList();
                    return View(db.AccountInfoes.Find(ac.Account_No));

                }
                if ( ac.Amount == 0)
                {
                    ViewBag.Accounts = db.AccountInfoes.ToList();
                    return View(db.AccountInfoes.Find(ac.Account_No));

                }

                AccountInfo accounts1 = db.AccountInfoes.Find(ac.Account_No);
            AccountInfo accounts2 = db.AccountInfoes.Find(ac.Account);
                accounts1.Balance = accounts1.Balance - ac.Amount;
                accounts2.Balance = accounts2.Balance + ac.Amount;
                db.SaveChanges();
            return Redirect("/Account/Index");
            }
            else
            {
                ViewBag.MyMessage = "Amount is Greater than Balance";
                ViewBag.Accounts = db.AccountInfoes.ToList();
                return View(db.AccountInfoes.Find(ac.Account_No));
            }

        }
    }
    public partial class AccountInfo1
    {
        public int Account_No { get; set; }
      
        public double Balance { get; set; }
       
        public string AccountType { get; set; }

        public int Account { get; set; }
        public float Amount { get; set; }
    }
}
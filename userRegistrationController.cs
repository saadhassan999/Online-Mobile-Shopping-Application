using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using myFirstApp_mobile_.Models;
namespace myFirstApp_mobile_.Controllers
{
    public class userRegistrationController : Controller
    {
        mobileContext db = new mobileContext();
        // GET: userRegistration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(userRegistration user)
        {
            if (ModelState.IsValid)
            {
              
                if(db.userRegistrations.Any(x=>x.emailAddress == user.emailAddress))
                {
                    ViewBag.ErrorMessage = "email address already exist";
                }
                else {
                    db.userRegistrations.Add(user);
                    db.SaveChanges();
                    ViewBag.Message = "Registered Successfully ;)";
                   
                }

                ModelState.Clear();

            }
            return View(user);

        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult login(userRegistration user)
        {
            if (ModelState.IsValid)
            {

                if (user.is)
                {

                }


                else
                {
                    ModelState.AddModelError("", "invalid credentials");
                }




            }
            return View(user);
        }


    }
}
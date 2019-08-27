using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myFirstApp_mobile_.Models;
using System.Configuration;
using System.Web.Util;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace myFirstApp_mobile_.Controllers
{
    public class MobileController : Controller
    {
        mobileContext mc = new mobileContext();
        
        // GET: Mobile
        public ActionResult Index(string textSelect, string search)
        {
            DbSet<mobile> s = mc.mobiles;
            var q = from mob in mc.mobiles
                    join bra in mc.brands
                    on mob.brand_id equals bra.brand_id
                    select new
                    {
                        brand_name = bra.brand_name
                    };
            if (textSelect == "mobileName")
            {
                return View(mc.mobiles.Where(x => x.mobile_name == search || search == null).ToList());
            }
            else if (textSelect == "mobilePrice")
            {
                return View(mc.mobiles.Where(x => x.mobile_price.ToString() == search || search == null).ToList());
            }
            else if (textSelect == "mobileModel")
            {
                return View(mc.mobiles.Where(x => x.mobile_model == search || search == null).ToList());
            }
            else if(textSelect == "brandName")
            {
                return View(mc.mobiles.Where(x => x.brand.brand_name == search || search == null).ToList());
            }
            else
            {
                return View(s.ToList());
            }
           
            

          
            

        }
        
        // create mobile
        public ActionResult addMobile()
        {
          
            ViewBag.Bra = new SelectList(mc.brands, "brand_id","brand_name");
            return View();
        }

        [HttpPost]
        public ActionResult addMobile(mobile mb)
        {
            
            if (ModelState.IsValid)
            {
                mc.Entry(mb).State = EntityState.Added;
               
                    mc.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        // delete mobile action
        public ActionResult Delete(int id)
        {
            mobile mb = mc.mobiles.Single(x => x.mobile_id == id);
            if (mb == null)
            {
                return HttpNotFound();
            }

            return View(mb);
        }
        [HttpPost]
        public ActionResult Delete(mobile mob)
        {
            if (ModelState.IsValid) { 
            mc.Entry(mob).State = EntityState.Deleted;
            mc.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
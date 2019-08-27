using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myFirstApp_mobile_.Models;
namespace myFirstApp_mobile_.Controllers
{
    public class BrandController : Controller
    {
         mobileContext mc = new mobileContext();
        // brand list   
        public ActionResult Index()
        {
            var s = mc.brands;
            return View(s.ToList());
        }

        // add new brand
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(brand b)
        {

            if (ModelState.IsValid)
            {
                mc.Entry(b).State = EntityState.Added;
                mc.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
          
        }


        // edit exsisting brand

        public ActionResult Edit(int id)
        {
            brand br = mc.brands.Single(s => s.brand_id == id);
            if(br == null)
            {
                return HttpNotFound();
            }
            return View(br);
        }

        [HttpPost]
        public ActionResult Edit(brand brandObj)
        {
          
            if (ModelState.IsValid)
            {
                
              
                    mc.Entry(brandObj).State = EntityState.Modified;
                    mc.SaveChanges();
              
                return RedirectToAction("Index");
            }
            return View();
        }

        // delete brand record
        public ActionResult Delete(int id)
        {
            brand b = mc.brands.Single(x => x.brand_id == id);
            if(b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost]
        public ActionResult Delete(brand brObj)
        {
            if (ModelState.IsValid)
            {
                mc.Entry(brObj).State = EntityState.Deleted;

                mc.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    } 
}
using MOCKDAL.Helper;
using MOCKDAL.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProj.ViewModel;

namespace TestProj.Controllers
{
    public class MOCKTBLController : Controller
    {
        // GET: CustomerList 
        [HttpGet]
        public ActionResult Index()
        {
            List<MOCKTBLViewModel> mTbl = Common.GetEntries().Select(x => new MOCKTBLViewModel { FirstName = x.FirstName, LastName = x.LastName, Id = x.Id, Email = x.Email }).ToList();            
            //ViewBag.MOCKTBLList = mTbl;
            return View(mTbl);
        }
        //[HttpPost]
        //public ActionResult Index(MOCKTBLViewModel cust)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(false, JsonRequestBehavior.DenyGet);
        //    }
        //    MOCKTBL cl = new MOCKTBL
        //    {
        //        FirstName = cust.FirstName,
        //        LastName = cust.LastName,
        //        Email = cust.Email,
        //        Id = cust.Id
        //    };
        //    Common.SaveRecord(cl);
            
        //    return View(cust);
        //}
        [HttpPost]
        public JsonResult UpdateRec(MOCKTBLViewModel cust)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = false, Id = 0 }, JsonRequestBehavior.DenyGet);
            }
            MOCKTBL cl = new MOCKTBL
            {
                FirstName = cust.FirstName,
                LastName = cust.LastName, 
                Email = cust.Email,
                Id = cust.Id
            };
            int id= Common.SaveRecord(cl);

            //return View(cust);
            return Json(new { result = true, Id = id }, JsonRequestBehavior.AllowGet);;
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            var cust = new MOCKTBLViewModel();
            ViewBag.Action = "Add";
            return PartialView("Update", cust);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cust = new MOCKTBLViewModel();
            if (id != 0)
            {
                var c = Common.GetEntryByID(id);
                cust.FirstName = c.FirstName;
                cust.LastName = c.LastName;
                cust.Email = c.Email;
                cust.Id = c.Id;
            }
            ViewBag.Action = "Edit";
            return PartialView("Update", cust);
        }
        
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            bool result = false;

            result = Common.DeleteEntry(Id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
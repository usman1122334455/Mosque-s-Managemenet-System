using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mini_project.Models;

namespace mini_project.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        prayDataContext pdc = new prayDataContext();
        public ActionResult Index()
        {
            List<string> areas = pdc.prayers.Select(ma => ma.masjid_area).ToList();
            List<string> areas1 = new HashSet<string>(areas).ToList();
            ViewData["areas"] = areas1;
            return View();
        }
        public ActionResult add()
        {
            string masjid = Request["masjid"];
            string loc = Request["location"];
            string ft = Request["f"];
            string zt = Request["z"];
            string at = Request["a"];
            string mt = Request["m"];
            string et = Request["e"];
            prayer p = new prayer();
            p.masjid_area = loc;
            p.masjid_name = masjid;
            p.fajr_time = ft;
            p.zuhr_time = zt;
            p.asar_time = at;
            p.magrib_time = mt;
            p.esha_time = et;
            pdc.prayers.InsertOnSubmit(p);
            pdc.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult find()
        {
            string area = Request["areas"];
            var n = pdc.prayers.Where(ar=>ar.masjid_area==area).ToList();
            return View(n);
        }
        public ActionResult EditDone(int id)
        {
            return View(pdc.prayers.First(ar=>ar.Id==id));
        }
        public ActionResult editOK(int id)
        {
            var a = pdc.prayers.First(ar => ar.Id == id);
            a.masjid_area = Request["ml"];
            a.masjid_name = Request["mn"];
            a.fajr_time = Request["ft"];
            a.zuhr_time = Request["zt"];
            a.asar_time = Request["at"];
            a.magrib_time = Request["mt"];
            a.esha_time = Request["et"];
            pdc.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}

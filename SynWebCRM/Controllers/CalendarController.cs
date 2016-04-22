using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using SynWebCRM.Data;
using SynWebCRM.Security;

namespace SynWebCRM.Controllers
{
    [Authorize(Roles = CRMRoles.Admin)]
    public class CalendarController: Controller
    {
        Model db = new Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            var model = new Event {StartDate = DateTime.Now};
            return View(model);
        }


        // GET: Deals/Details/5
        public ActionResult EventDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ev = db.Events.Find(id);
            if (ev == null)
            {
                return HttpNotFound();
            }
            return View(ev);
        }

        // GET: Websites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent([Bind(Include = "EventId,StartDate,EndDate,Name,Description")] Event ev)
        {
            if (ModelState.IsValid)
            {
                ev.CreationDate = DateTime.Now;
                db.Events.Add(ev);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(ev);
        }

        public ActionResult EditEvent(int? id)
        {   
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ev = db.Events.Find(id);
            if (ev == null)
            {
                return HttpNotFound();
            }
            return View(ev);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent([Bind(Include = "EventId,CreationDate,StartDate,EndDate,Name,Description")] Event ev)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ev);
        }

        // GET: Deals/Delete/5
        public ActionResult DeleteEvent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rec = db.Events.Find(id);
            if (rec == null)
            {
                return HttpNotFound();
            }
            return View(rec);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("DeleteEvent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rec = db.Events.Find(id);
            db.Events.Remove(rec);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    
}
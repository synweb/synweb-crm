using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.Controllers
{
    [Authorize(Roles = CRMRoles.Admin)]
    public class CalendarController: Controller
    {
        public CalendarController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        //private readonly CRMModel _crmModel;
        private IEventRepository _eventRepository;

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
                return StatusCode(400);
            }
            var ev = _eventRepository.GetById(id.Value);
            if (ev == null)
            {
                return StatusCode(404);
            }
            return View(ev);
        }

        // GET: Websites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent([Bind("EventId,StartDate,EndDate,Name,Description")] Event ev)
        {
            if (ModelState.IsValid)
            {
                _eventRepository.Add(ev);
                return RedirectToAction("Index");
            }
            
            return View(ev);
        }

        public ActionResult EditEvent(int? id)
        {   
            if (id == null)
            {
                return StatusCode(400);
            }
            var ev = _eventRepository.GetById(id.Value);
            if (ev == null)
            {
                return StatusCode(404);
            }
            return View(ev);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent([Bind("EventId,CreationDate,StartDate,EndDate,Name,Description")] Event ev)
        {
            if (ModelState.IsValid)
            {
                _eventRepository.Update(ev);
                return RedirectToAction("Index");
            }
            return View(ev);
        }

        // GET: Deals/Delete/5
        public ActionResult DeleteEvent(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            var rec = _eventRepository.GetById(id.Value);
            if (rec == null)
            {
                return StatusCode(400);
            }
            return View(rec);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("DeleteEvent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
    
}
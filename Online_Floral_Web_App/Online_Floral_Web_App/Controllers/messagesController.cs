using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Floral_Web_App.Models;

namespace Online_Floral_Web_App.Controllers
{
    public class messagesController : Controller
    {
        private online_floralEntities db = new online_floralEntities();

        // GET: messages
        public ActionResult Index()
        {
            var messages = db.messages.Include(m => m.Occasion);
            return View(messages.ToList());
        }

        // GET: messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            messages messages = db.messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // GET: messages/Create
        public ActionResult Create()
        {
            ViewBag.Occasionid = new SelectList(db.Occasion, "Occasionid", "name");
            return View();
        }

        // POST: messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Occasionid,message")] messages messages)
        {
            if (ModelState.IsValid)
            {
                db.messages.Add(messages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Occasionid = new SelectList(db.Occasion, "Occasionid", "name", messages.Occasionid);
            return View(messages);
        }

        // GET: messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            messages messages = db.messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            ViewBag.Occasionid = new SelectList(db.Occasion, "Occasionid", "name", messages.Occasionid);
            return View(messages);
        }

        // POST: messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Occasionid,message")] messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Occasionid = new SelectList(db.Occasion, "Occasionid", "name", messages.Occasionid);
            return View(messages);
        }

        // GET: messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            messages messages = db.messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            messages messages = db.messages.Find(id);
            db.messages.Remove(messages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

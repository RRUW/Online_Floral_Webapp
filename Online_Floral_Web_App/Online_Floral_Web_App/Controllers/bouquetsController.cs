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
    public class bouquetsController : Controller
    {
        private online_floralEntities db = new online_floralEntities();

        // GET: bouquets
        public ActionResult Index()
        {
            var bouquet = db.bouquet.Include(b => b.category).Include(b => b.Occasion);
            return View(bouquet.ToList());
        }

        // GET: bouquets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bouquet bouquet = db.bouquet.Find(id);
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            return View(bouquet);
        }

        // GET: bouquets/Create
        public ActionResult Create()
        {
            ViewBag.cate_id = new SelectList(db.category, "Id", "Category_name");
            ViewBag.occasion_id = new SelectList(db.Occasion, "Occasionid", "name");
            return View();
        }

        // POST: bouquets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bouquetid,name,price,image,cate_id,occasion_id")] bouquet bouquet)
        {
            if (ModelState.IsValid)
            {
                db.bouquet.Add(bouquet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cate_id = new SelectList(db.category, "Id", "Category_name", bouquet.cate_id);
            ViewBag.occasion_id = new SelectList(db.Occasion, "Occasionid", "name", bouquet.occasion_id);
            return View(bouquet);
        }

        // GET: bouquets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bouquet bouquet = db.bouquet.Find(id);
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            ViewBag.cate_id = new SelectList(db.category, "Id", "Category_name", bouquet.cate_id);
            ViewBag.occasion_id = new SelectList(db.Occasion, "Occasionid", "name", bouquet.occasion_id);
            return View(bouquet);
        }

        // POST: bouquets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bouquetid,name,price,image,cate_id,occasion_id")] bouquet bouquet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bouquet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cate_id = new SelectList(db.category, "Id", "Category_name", bouquet.cate_id);
            ViewBag.occasion_id = new SelectList(db.Occasion, "Occasionid", "name", bouquet.occasion_id);
            return View(bouquet);
        }

        // GET: bouquets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bouquet bouquet = db.bouquet.Find(id);
            if (bouquet == null)
            {
                return HttpNotFound();
            }
            return View(bouquet);
        }

        // POST: bouquets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bouquet bouquet = db.bouquet.Find(id);
            db.bouquet.Remove(bouquet);
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

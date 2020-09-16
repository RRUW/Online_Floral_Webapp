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
    public class salesController : Controller
    {
        private online_floralEntities db = new online_floralEntities();

        // GET: sales
        public ActionResult Index()
        {
            var sales = db.sales.Include(s => s.bouquet).Include(s => s.customer);
            return View(sales.ToList());
        }

        // GET: sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sales sales = db.sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: sales/Create
        public ActionResult Create()
        {
            ViewBag.bouquetid = new SelectList(db.bouquet, "bouquetid", "name");
            ViewBag.custid = new SelectList(db.customer, "custid", "first_name");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,custid,bouquetid,purchase_date")] sales sales)
        {
            if (ModelState.IsValid)
            {
                db.sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bouquetid = new SelectList(db.bouquet, "bouquetid", "name", sales.bouquetid);
            ViewBag.custid = new SelectList(db.customer, "custid", "first_name", sales.custid);
            return View(sales);
        }

        // GET: sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sales sales = db.sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.bouquetid = new SelectList(db.bouquet, "bouquetid", "name", sales.bouquetid);
            ViewBag.custid = new SelectList(db.customer, "custid", "first_name", sales.custid);
            return View(sales);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,custid,bouquetid,purchase_date")] sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bouquetid = new SelectList(db.bouquet, "bouquetid", "name", sales.bouquetid);
            ViewBag.custid = new SelectList(db.customer, "custid", "first_name", sales.custid);
            return View(sales);
        }

        // GET: sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sales sales = db.sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sales sales = db.sales.Find(id);
            db.sales.Remove(sales);
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

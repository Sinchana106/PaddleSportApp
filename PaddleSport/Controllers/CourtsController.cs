using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaddleSport.Models;

namespace PaddleSport.Controllers
{
    public class CourtsController : Controller
    {
        private PaddleSportContext db = new PaddleSportContext();

        // GET: Courts
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var court = db.court.Include(c => c.Loc);
            return View(court.ToList());
        }

        // GET: Courts/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.court.Find(id);
            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

        // GET: Courts/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.location, "Id", "Name");
            return View();
        }

        // POST: Courts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,LocationId,Name,PricePerHour")] Court court)
        {
            if (ModelState.IsValid)
            {
                db.court.Add(court);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.location, "Id", "Name", court.LocationId);
            return View(court);
        }

        // GET: Courts/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.court.Find(id);
            if (court == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.location, "Id", "Name", court.LocationId);
            return View(court);
        }

        // POST: Courts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,LocationId,Name,PricePerHour")] Court court)
        {
            if (ModelState.IsValid)
            {
                db.Entry(court).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.location, "Id", "Name", court.LocationId);
            return View(court);
        }

        // GET: Courts/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.court.Find(id);
            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }
        [Authorize(Roles = "admin")]

        // POST: Courts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Court court = db.court.Find(id);
            db.court.Remove(court);
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

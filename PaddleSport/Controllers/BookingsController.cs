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
    public class BookingsController : Controller
    {
        private PaddleSportContext db = new PaddleSportContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var booking = db.booking.Include(b => b.CourtFk).Include(b => b.Loc);
            return View(booking.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CourtId = new List<SelectListItem>
            {
                new SelectListItem{Text="Select",Value="Select" }
            };

            ViewBag.LocationId = new SelectList(db.location, "Id", "Name");
            return View();
        }

        public JsonResult GetCourts(int id)
        {
            var courts = db.court.Where(x => x.LocationId == id).Select(x => new
            {
                id = x.Id,
                text = x.Name
            });
            return Json(courts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotal(int id)
        {
            var total = db.court.Single(x => x.Id == id).PricePerHour;
            return Json(total, JsonRequestBehavior.AllowGet);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationId,Timings,NoOfHours,CourtId,NoOfPlayers,total")] Booking booking,Court c)
        {
            if (ModelState.IsValid)
            {
               booking.EndTime= booking.Timings.AddHours(booking.NoOfHours);
                db.booking.Add(booking);
                db.SaveChanges();
                Session["total"] = booking.total;
                return RedirectToAction("PaymentDetails","Payment");

            }
            try
            {
                if (booking.NoOfHours > 0)
                {
                    booking.total = (booking.NoOfHours) * (c.PricePerHour);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
            ViewBag.CourtId = new SelectList(db.court, "Id", "Name", booking.CourtId);
            ViewBag.LocationId = new SelectList(db.location, "Id", "Name", booking.LocationId);
            ViewBag.tcost = booking.total;
            
            return View(booking);
        }
        [Authorize(Roles = "admin")]
        //GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(db.court, "Id", "Name", booking.CourtId);
            ViewBag.LocationId = new SelectList(db.location, "Id", "Name", booking.LocationId);
            return View(booking);
        }
        [Authorize(Roles = "admin")]
        //POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationId,Timings,NoOfHours,CourtId,NoOfPlayers,total")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourtId = new SelectList(db.court, "Id", "Name", booking.CourtId);
            ViewBag.LocationId = new SelectList(db.location, "Id", "Name", booking.LocationId);
            return View(booking);
        }
        [Authorize(Roles = "admin")]
        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
        [Authorize(Roles = "admin")]
        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.booking.Find(id);
            db.booking.Remove(booking);
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

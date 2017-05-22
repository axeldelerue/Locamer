using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLocamer.Models;

namespace WebLocamer.Controllers
{
    public class SejoursController : Controller
    {
        private LocamerEntities db = new LocamerEntities();

        // GET: Sejours
        public ActionResult Index()
        {
            var sejours = db.Sejours.Include(s => s.Client).Include(s => s.type_sejour);
            return View(sejours.ToList());
        }

        // GET: Sejours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejour sejour = db.Sejours.Find(id);
            if (sejour == null)
            {
                return HttpNotFound();
            }
            return View(sejour);
        }

        // GET: Sejours/Create
        public ActionResult Create()
        {
            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom");
            ViewBag.id_tsejour = new SelectList(db.type_sejour, "id_type", "lib_sejour");
            return View();
        }

        // POST: Sejours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_sejour,date_debut,date_fin,id_client,id_tsejour,id_option")] Sejour sejour)
        {
            if (ModelState.IsValid)
            {
                db.Sejours.Add(sejour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom", sejour.id_client);
            ViewBag.id_tsejour = new SelectList(db.type_sejour, "id_type", "lib_sejour", sejour.id_tsejour);
            return View(sejour);
        }

        // GET: Sejours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejour sejour = db.Sejours.Find(id);
            if (sejour == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom", sejour.id_client);
            ViewBag.id_tsejour = new SelectList(db.type_sejour, "id_type", "lib_sejour", sejour.id_tsejour);
            return View(sejour);
        }

        // POST: Sejours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_sejour,date_debut,date_fin,id_client,id_tsejour,id_option")] Sejour sejour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sejour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_client = new SelectList(db.Clients, "id_client", "nom", sejour.id_client);
            ViewBag.id_tsejour = new SelectList(db.type_sejour, "id_type", "lib_sejour", sejour.id_tsejour);
            return View(sejour);
        }

        // GET: Sejours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejour sejour = db.Sejours.Find(id);
            if (sejour == null)
            {
                return HttpNotFound();
            }
            return View(sejour);
        }

        // POST: Sejours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sejour sejour = db.Sejours.Find(id);
            db.Sejours.Remove(sejour);
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

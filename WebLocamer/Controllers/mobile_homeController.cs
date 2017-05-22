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
    public class mobile_homeController : Controller
    {
        private LocamerEntities db = new LocamerEntities();

        // GET: mobile_home
        public ActionResult Index()
        {
            var mobile_home = db.mobile_home.Include(m => m.tarif);
            return View(mobile_home.ToList());
        }

        // GET: mobile_home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mobile_home mobile_home = db.mobile_home.Find(id);
            if (mobile_home == null)
            {
                return HttpNotFound();
            }
            return View(mobile_home);
        }

        // GET: mobile_home/Create
        public ActionResult Create()
        {
            ViewBag.id_tarif = new SelectList(db.tarifs, "id_tarif", "libelle");
            return View();
        }

        // POST: mobile_home/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mobile_home,code_emplacement,capacité,id_tarif,terrasse_couverte")] mobile_home mobile_home)
        {
            if (ModelState.IsValid)
            {
                db.mobile_home.Add(mobile_home);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_tarif = new SelectList(db.tarifs, "id_tarif", "libelle", mobile_home.id_tarif);
            return View(mobile_home);
        }

        // GET: mobile_home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mobile_home mobile_home = db.mobile_home.Find(id);
            if (mobile_home == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_tarif = new SelectList(db.tarifs, "id_tarif", "libelle", mobile_home.id_tarif);
            return View(mobile_home);
        }

        // POST: mobile_home/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mobile_home,code_emplacement,capacité,id_tarif,terrasse_couverte")] mobile_home mobile_home)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mobile_home).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tarif = new SelectList(db.tarifs, "id_tarif", "libelle", mobile_home.id_tarif);
            return View(mobile_home);
        }

        // GET: mobile_home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mobile_home mobile_home = db.mobile_home.Find(id);
            if (mobile_home == null)
            {
                return HttpNotFound();
            }
            return View(mobile_home);
        }

        // POST: mobile_home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mobile_home mobile_home = db.mobile_home.Find(id);
            db.mobile_home.Remove(mobile_home);
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

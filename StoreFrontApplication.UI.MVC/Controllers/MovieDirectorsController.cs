using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontApplication.DATA.EF;

namespace StoreFrontApplication.UI.MVC.Controllers
{
    public class MovieDirectorsController : Controller
    {
        private StoreFrontApplicationEntities db = new StoreFrontApplicationEntities();

        // GET: MovieDirectors
        public ActionResult Index()
        {
            var movieDirectors = db.MovieDirectors.Include(m => m.Director).Include(m => m.Movy);
            return View(movieDirectors.ToList());
        }

        // GET: MovieDirectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDirector movieDirector = db.MovieDirectors.Find(id);
            if (movieDirector == null)
            {
                return HttpNotFound();
            }
            return View(movieDirector);
        }

        // GET: MovieDirectors/Create
        public ActionResult Create()
        {
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName");
            ViewBag.MovieID = new SelectList(db.Movies1, "MovieID", "MovieTitle");
            return View();
        }

        // POST: MovieDirectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieDirectorID,MovieID,DirectorID")] MovieDirector movieDirector)
        {
            if (ModelState.IsValid)
            {
                db.MovieDirectors.Add(movieDirector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName", movieDirector.DirectorID);
            ViewBag.MovieID = new SelectList(db.Movies1, "MovieID", "MovieTitle", movieDirector.MovieID);
            return View(movieDirector);
        }

        // GET: MovieDirectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDirector movieDirector = db.MovieDirectors.Find(id);
            if (movieDirector == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName", movieDirector.DirectorID);
            ViewBag.MovieID = new SelectList(db.Movies1, "MovieID", "MovieTitle", movieDirector.MovieID);
            return View(movieDirector);
        }

        // POST: MovieDirectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieDirectorID,MovieID,DirectorID")] MovieDirector movieDirector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieDirector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName", movieDirector.DirectorID);
            ViewBag.MovieID = new SelectList(db.Movies1, "MovieID", "MovieTitle", movieDirector.MovieID);
            return View(movieDirector);
        }

        // GET: MovieDirectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDirector movieDirector = db.MovieDirectors.Find(id);
            if (movieDirector == null)
            {
                return HttpNotFound();
            }
            return View(movieDirector);
        }

        // POST: MovieDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDirector movieDirector = db.MovieDirectors.Find(id);
            db.MovieDirectors.Remove(movieDirector);
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

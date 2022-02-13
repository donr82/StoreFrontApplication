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
    public class MoviesController : Controller
    {
        private StoreFrontApplicationEntities db = new StoreFrontApplicationEntities();

        // GET: Movies
        public ActionResult Index()
        {
            var movies1 = db.Movies1.Include(m => m.Genre).Include(m => m.MovieStatu).Include(m => m.Rating);
            return View(movies1.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies1.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.MovieStatusID = new SelectList(db.MovieStatus, "MovieStatusID", "StatusName");
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "Rating1");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,MovieTitle,Price,UnitsSold,ReleaseDate,RatingID,GenreID,MovieStatusID,MovieImage,Description")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies1.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", movie.GenreID);
            ViewBag.MovieStatusID = new SelectList(db.MovieStatus, "MovieStatusID", "StatusName", movie.MovieStatusID);
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "Rating1", movie.RatingID);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies1.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", movie.GenreID);
            ViewBag.MovieStatusID = new SelectList(db.MovieStatus, "MovieStatusID", "StatusName", movie.MovieStatusID);
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "Rating1", movie.RatingID);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,MovieTitle,Price,UnitsSold,ReleaseDate,RatingID,GenreID,MovieStatusID,MovieImage,Description")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", movie.GenreID);
            ViewBag.MovieStatusID = new SelectList(db.MovieStatus, "MovieStatusID", "StatusName", movie.MovieStatusID);
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "Rating1", movie.RatingID);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies1.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies1.Find(id);
            db.Movies1.Remove(movie);
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

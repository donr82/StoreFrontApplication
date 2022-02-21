using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontApplication.DATA.EF;
using StoreFrontApplication.UI.MVC.Models;

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

        public ActionResult AddToCart(int qty, int movieID)
        {
            //Create an empty shell for a local (local to this method) shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if the session-cart variable actually exists; if so then there were already items in the shopping cart and we need to put 
            //those pre-existing items in the local shoppingCart collection we created above
            if (Session["cart"] != null)
            {
                //session cart exists - put its items in the local shoppingCart collection so that they are easier to work with
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //This is unboxing. Session object gets cast back to its original, more specific type. This is explicit casting
            }
            else
            {
                //if session cart doesnt exist yet, we need to instntiate it. (AKA new it up)
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            //find the product the user is trying to add to the cart
            Movie product = db.Movies1.Where(b => b.MovieID == movieID).FirstOrDefault();

            if (product == null)
            {
                //if a bad ID was passed to this method, we will kick the user back to some page to try again or we could throw an error.
                return RedirectToAction("Action");
            }
            else
            {
                //if bookID is valid, we are going to add line-item to cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //put the item in the local shoppingCart collection. BUT if we already have that product as a cart-item, then we will
                //update the qty only
                if (shoppingCart.ContainsKey(product.MovieID))
                {
                    shoppingCart[product.MovieID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.MovieID, item);
                }

                //Now update the session version of the cart so we can maintain that information between Request and Response cycles
                Session["cart"] = shoppingCart; //implicit casting aka boxing
            }

            //send them to a view that shows the list of all items in the cart
            return RedirectToAction("Index", "ShoppingCart");
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

using StoreFrontApplication.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFrontApplication.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            //Pull the session based shopping cart into a local variable that we can pass to the view
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //The user has either not put anything in the cart OR removed it all OR the session expired
                //Set the cart to an empty object. We can still send this to the view unlike null.
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                ViewBag.Message = "There are no items in your cart.";
            }
            else
            {
                ViewBag.Message = null; //Explicitly clearing out the ViewBag variable
            }
            return View(shoppingCart);
        }

        //*********** Shopping Cart Step 6. Step 5 on index for shopping cart
        public ActionResult RemoveFromCart(int id)
        {
            //We are going to get the shopping Cart out of the session variable and into a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //Remove the item
            shoppingCart.Remove(id);

            //Update Session
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        //*********** Shopping Cart Step 7. Step 8 on shared Layout
        public ActionResult UpdateCart(int movieID, int qty)
        {
            //We are going to get the shopping Cart out of the session variable and into a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //Target the correct cart item using the bookID and update its quantity
            shoppingCart[movieID].Qty = qty;

            //Update Session
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }
    }
}
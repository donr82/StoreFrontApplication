using StoreFrontApplication.DATA.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreFrontApplication.UI.MVC.Models
{
    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        public Movie Product { get; set; }
    

        public CartItemViewModel(int qty, Movie product)
        {
            Qty = qty;
            Product = product;
        }
    }
}
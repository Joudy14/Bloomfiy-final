﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;

namespace Bloomfiy_final.Controllers
{
    public class WishlistController : Controller
    {
        private const string SESSION_WISHLIST = "Bloomfiy.Wishlist";

        public ActionResult Index()
        {
            var wishlist = Session[SESSION_WISHLIST] as List<WishlistItem> ?? new List<WishlistItem>();
            return View(wishlist);
        }

        [HttpPost]
        public ActionResult Add(int id, string name, decimal price, string color, string imageUrl)
        {
            var wishlist = Session[SESSION_WISHLIST] as List<WishlistItem> ?? new List<WishlistItem>();

            // Avoid duplicates for same product+color
            if (!wishlist.Any(x => x.ProductId == id && x.Color == color))
            {
                wishlist.Add(new WishlistItem
                {
                    ProductId = id,
                    Name = name,
                    Price = price,
                    Color = color,
                    ImageUrl = imageUrl
                });
            }

            Session[SESSION_WISHLIST] = wishlist;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(int id, string color)
        {
            var wishlist = Session[SESSION_WISHLIST] as List<WishlistItem> ?? new List<WishlistItem>();
            var item = wishlist.FirstOrDefault(x => x.ProductId == id && x.Color == color);

            if (item != null)
            {
                wishlist.Remove(item);
            }

            Session[SESSION_WISHLIST] = wishlist;

            return RedirectToAction("Index");
        }
    }
}
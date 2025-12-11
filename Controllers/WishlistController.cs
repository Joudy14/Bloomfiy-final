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
        public JsonResult AddAjax(int id, string name, decimal price, string color, string imageUrl)
        {
            var wishlist = Session["Bloomfiy.Wishlist"] as List<WishlistItem> ?? new List<WishlistItem>();

            if (!wishlist.Any(x => x.ProductId == id))
            {
                wishlist.Add(new WishlistItem
                {
                    ProductId = id,
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                });
            }

            Session["Bloomfiy.Wishlist"] = wishlist;

            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult Remove(int id, string color)
        {
            var wishlist = Session[SESSION_WISHLIST] as List<WishlistItem> ?? new List<WishlistItem>();
            var item = wishlist.FirstOrDefault(x => x.ProductId == id );

            if (item != null)
            {
                wishlist.Remove(item);
            }

            Session[SESSION_WISHLIST] = wishlist;

            return RedirectToAction("Index");
        }

        public bool IsInWishlist(int productId)
        {
            var wishlist = Session["Bloomfiy.Wishlist"] as List<WishlistItem>;
            return wishlist != null && wishlist.Any(x => x.ProductId == productId);
        }

        [HttpPost]
        public JsonResult Toggle(int id, string name, decimal price, string imageUrl)
        {
            var wishlist = Session["Bloomfiy.Wishlist"] as List<WishlistItem>
                           ?? new List<WishlistItem>();

            var existing = wishlist.FirstOrDefault(x => x.ProductId == id);

            bool added;

            if (existing != null)
            {
                wishlist.Remove(existing);
                added = false;
            }
            else
            {
                wishlist.Add(new WishlistItem
                {
                    ProductId = id,
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                });
                added = true;
            }

            Session["Bloomfiy.Wishlist"] = wishlist;

            return Json(new { success = true, added });
        }

    }
}
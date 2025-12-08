using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Bloomfiy_final.Models;

namespace Bloomfiy_final.Controllers
{

    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product/Catalog
        public ActionResult Catalog()
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.IsAvailable)
                .OrderBy(p => p.Name)
                .ToList();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();

            return View(products);
        }

        // GET: Product/Details
        public ActionResult Details(int id)
        {
            var product = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

          
            var relatedProducts = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id && p.IsAvailable)
                .Take(4)
                .ToList();

            ViewBag.RelatedProducts = relatedProducts;
            return View(product);
        }
    }
}
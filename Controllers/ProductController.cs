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

            ViewBag.Categories = db.Categories.ToList();   // ✅ REQUIRED
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList(); // ✅ REQUIRED

            return View(products);
        }

        // GET: Product/Filter (AJAX)
        public ActionResult Filter(
            List<int> categories,
            List<int> colors,
            decimal maxPrice,
            string sort,
            string search)
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.IsAvailable && p.BasePrice <= maxPrice);

            if (categories != null && categories.Any())
                products = products.Where(p => categories.Contains(p.CategoryId));

            if (colors != null && colors.Any())
                products = products.Where(p =>
                    p.ProductColors.Any(pc => colors.Contains(pc.ColorId)));

            if (!string.IsNullOrWhiteSpace(search))
                products = products.Where(p => p.Name.Contains(search));

            switch (sort)
            {
                case "name-asc":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "price-asc":
                    products = products.OrderBy(p => p.BasePrice);
                    break;
                case "price-desc":
                    products = products.OrderByDescending(p => p.BasePrice);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return PartialView("_ProductGrid", products.ToList());
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
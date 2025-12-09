using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;
using System.Data.Entity;

namespace Bloomfiy_final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // ========================= INDEX =========================
        public ActionResult Index()
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors)
                .ToList();

            return View("~/Views/Admin/AdminProduct/Index.cshtml", products);
        }

        // ========================= CREATE (GET) =========================
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
            return View("~/Views/Admin/AdminProduct/Create.cshtml");
        }

        // ========================= CREATE (POST) =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            Product product,
            int[] selectedColors,
            Dictionary<int, string> colorImageNames,
            string productInfoImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
                return View("~/Views/Admin/AdminProduct/Create.cshtml", product);
            }

            product.DateCreated = DateTime.Now;
            product.InfoImage = productInfoImage;

            db.Products.Add(product);
            db.SaveChanges();

            // Create image folder
            string folderPath = Server.MapPath($"~/Images/products_img/{product.ImageFolderName}/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Save colors
            // ✅ Save colors with correct image filenames
            if (selectedColors != null)
            {
                foreach (var colorId in selectedColors)
                {
                    string formKey = $"colorImageNames[{colorId}]";
                    string imageName = Request.Form[formKey];

                    // ✅ If admin typed filename
                    if (!string.IsNullOrWhiteSpace(imageName))
                    {
                        imageName = imageName.Trim();
                    }
                    else
                    {
                        // ✅ Fallback using COLOR NAME (NOT ID)
                        var color = db.Colors.Find(colorId);
                        var cleanColorName = color.ColorName.ToLower().Replace(" ", "");
                        imageName = $"{product.ImageFolderName}_{cleanColorName}.jpg";
                    }

                    db.ProductColors.Add(new ProductColor
                    {
                        ProductId = product.ProductId,
                        ColorId = colorId,
                        ImageFileName = imageName
                    });
                }

                db.SaveChanges();
            }


            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction("Index");
        }

        // ========================= EDIT (GET) =========================
        public ActionResult Edit(int id)
        {
            var product = db.Products
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return HttpNotFound();

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();

            return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
        }

        // ========================= EDIT (POST) =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, int[] selectedColors, string productInfoImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
                return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
            }

            // ✅ Update main product fields
            var dbProduct = db.Products.Find(product.ProductId);
            if (dbProduct == null)
                return HttpNotFound();

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.BasePrice = product.BasePrice;
            dbProduct.StockQuantity = product.StockQuantity;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.IsAvailable = product.IsAvailable;
            dbProduct.InfoImage = productInfoImage;

            db.SaveChanges();

            // ✅ Save existing image filenames BEFORE deleting
            var existingImages = db.ProductColors
                .Where(pc => pc.ProductId == product.ProductId)
                .ToDictionary(pc => pc.ColorId, pc => pc.ImageFileName);

            // ✅ Remove old ProductColors
            db.ProductColors.RemoveRange(
                db.ProductColors.Where(pc => pc.ProductId == product.ProductId)
            );
            db.SaveChanges();

            // ✅ Re-add selected colors with CORRECT filenames
            if (selectedColors != null)
            {
                foreach (var colorId in selectedColors)
                {
                    string formKey = $"colorImageNames[{colorId}]";
                    string imageName = Request.Form[formKey];

                    // 1️⃣ Take from form input
                    if (!string.IsNullOrWhiteSpace(imageName))
                        imageName = imageName.Trim();

                    // 2️⃣ Fallback to existing filename
                    else if (existingImages.ContainsKey(colorId))
                        imageName = existingImages[colorId];

                    // 3️⃣ LAST fallback (should rarely happen)
                    else
                        imageName = $"{dbProduct.ImageFolderName}_{colorId}.jpg";

                    db.ProductColors.Add(new ProductColor
                    {
                        ProductId = dbProduct.ProductId,
                        ColorId = colorId,
                        ImageFileName = imageName
                    });
                }

                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction("Index");
        }


        // ========================= DELETE =========================
        public ActionResult Delete(int id)
        {
            var product = db.Products
                .Include(p => p.ProductColors)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return HttpNotFound();

            return View("~/Views/Admin/AdminProduct/Delete.cshtml", product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                var colors = db.ProductColors.Where(pc => pc.ProductId == id);
                db.ProductColors.RemoveRange(colors);
                db.Products.Remove(product);
                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction("Index");
        }

        // ========================= TOGGLE =========================
        public ActionResult ToggleAvailability(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                product.IsAvailable = !product.IsAvailable;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;
using Microsoft.AspNet.Identity;

namespace Bloomfiy_final.Controllers
{
    [Authorize]  // ⬅ user must be logged in
    public class CheckoutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const string SESSION_CART = "Bloomfiy.Cart";

        // GET: Checkout
        public ActionResult Index()
        {
            var cart = Session[SESSION_CART] as List<CartItem>;

            if (cart == null || !cart.Any())
            {
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            var model = new CheckoutViewModel
            {
                CartItems = cart,
                Total = cart.Sum(x => x.TotalPrice)
            };

            return View(model);
        }

        // POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessOrder(CheckoutInputModel input)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all fields correctly.";
                return RedirectToAction("Index");
            }

            var cart = Session[SESSION_CART] as List<CartItem>;
            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Cart empty.";
                return RedirectToAction("Index", "Cart");
            }

            // Create Order
            var order = new Order
            {
                UserId = User.Identity.GetUserId(),
                CustomerName = input.FullName,
                Address = input.Address,
                City = input.City,
                Phone = input.Phone,
                PaymentMethod = "Credit Card",
                CreatedAt = DateTime.Now,
                Status = "Pending",
                TotalAmount = cart.Sum(x => x.TotalPrice)
            };

            db.Orders.Add(order);
            db.SaveChanges();

            // Add Order Items
            foreach (var cartItem in cart)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                    Color = cartItem.Color,
                    ImageUrl = cartItem.ImageUrl,
                    HasBouquet = cartItem.HasBouquet
                };

                db.OrderItems.Add(orderItem);
            }

            db.SaveChanges();

            // Clear cart
            Session[SESSION_CART] = null;

            return RedirectToAction("Success", new { id = order.OrderId });
        }

        // ORDER SUCCESS PAGE
        public ActionResult Success(int id)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return RedirectToAction("Index", "Home");

            return View(order);
        }
    }
}

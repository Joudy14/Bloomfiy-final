using System;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;
using Microsoft.AspNet.Identity;

namespace Bloomfiy_final.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var cart = Session["Bloomfiy.Cart"] as System.Collections.Generic.List<CartItem>
                       ?? new System.Collections.Generic.List<CartItem>();

            CheckoutViewModel vm = new CheckoutViewModel
            {
                CartItems = cart,
                Input = new CheckoutInputModel()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult PlaceOrder(CheckoutInputModel input)
        {
            var cart = Session["Bloomfiy.Cart"] as System.Collections.Generic.List<CartItem>
                       ?? new System.Collections.Generic.List<CartItem>();

            if (!cart.Any())
                return RedirectToAction("Index", "Cart");

            if (string.IsNullOrWhiteSpace(input.FullName))
                ModelState.AddModelError("", "Name is required.");

            if (!ModelState.IsValid)
            {
                CheckoutViewModel vm = new CheckoutViewModel
                {
                    CartItems = cart,
                    Input = input
                };
                return View("Index", vm);
            }

            var order = new Order
            {
                UserId = User.Identity.GetUserId(),
                CustomerName = input.FullName,
                Address = input.Address,
                City = input.City,
                Phone = input.Phone,
                TotalAmount = cart.Sum(x => x.TotalPrice),
                Date = DateTime.Now,
                Status = "Pending",
                Items = cart.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    ProductName = x.Name,
                    Color = x.Color,
                    HasBouquet = x.HasBouquet,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            db.Orders.Add(order);
            db.SaveChanges();

            Session["Bloomfiy.Cart"] = null;

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}

using System;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;


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
        public ActionResult PlaceOrder(CheckoutViewModel model)
        {
            var cart = Session["Bloomfiy.Cart"] as List<CartItem>
                       ?? new List<CartItem>();

            if (!cart.Any())
                return RedirectToAction("Index", "Cart");

            if (string.IsNullOrWhiteSpace(model.Input.FullName))
                ModelState.AddModelError("", "Name is required.");

            if (!ModelState.IsValid)
            {
                model.CartItems = cart;
                return View("Index", model);
            }

            var order = new Order
            {
                UserId = User.Identity.GetUserId(),
                CustomerName = model.Input.FullName,
                Address = model.Input.Address,
                City = model.Input.City,
                Phone = model.Input.Phone,
                TotalAmount = cart.Sum(x => x.TotalPrice),
                Date = DateTime.Now,
                OrderDate = DateTime.Now,
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

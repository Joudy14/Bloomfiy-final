using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;

namespace Bloomfiy_final.Controllers
{
    public class CartController : Controller
    {

      
        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("CartController.Index hit!");
            var cart = Session["Bloomfiy.Cart"] as List<Bloomfiy_final.Models.CartItem> ?? new List<Bloomfiy_final.Models.CartItem>();
            return View(cart);
        }

        private const string SESSION_CART = "Bloomfiy.Cart";

        [HttpPost]
        public ActionResult Add(int id, string name, decimal price, string color, int quantity)
        {
            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();

        
            var item = cart.FirstOrDefault(x => x.ProductId == id && x.Color == color);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = id,
                    Name = name,
                    Price = price,
                    Color = color,
                    Quantity = quantity
                });
            }

            Session[SESSION_CART] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id, string color)
        {
            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == id && x.Color == color);
            if (item != null)
                cart.Remove(item);

            Session[SESSION_CART] = cart;
            return RedirectToAction("Index");
        }

  
        public ActionResult Clear()
        {
            Session[SESSION_CART] = null;
            return RedirectToAction("Index");
        }

     
        public ActionResult Confirm()
        {
            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();
            if (!cart.Any())
            {
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }

            Session["CheckoutCart"] = cart;

            Session[SESSION_CART] = null;

            return RedirectToAction("Index", "Checkout");
        }
    }
}

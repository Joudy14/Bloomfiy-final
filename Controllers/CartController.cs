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
            var cart = Session["Bloomfiy.Cart"] as List<CartItem>;

            if (cart == null || !cart.Any())
            {
                // Try restore from LocalStorage
                cart = new List<CartItem>();
                ViewBag.RestoreCartFromLocalStorage = true;
            }

            return View(cart);
        }


        private const string SESSION_CART = "Bloomfiy.Cart";

        [Authorize]
        [HttpPost]
        public ActionResult Add(int id, string name, decimal price, string color, int quantity, string imageUrl, bool hasBouquet)
        {

            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.ProductId == id && x.Name == name && x.Color == color);

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
                    Quantity = quantity,
                    ImageUrl = $"/Images/products_img/{name.ToLower().Replace(" ", "")}/{name.ToLower().Replace(" ", "")}_{color.ToLower()}.jpg"
                });
            }

            Session[SESSION_CART] = cart;
            return new HttpStatusCodeResult(200);
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

        [HttpPost]
        public ActionResult UpdateQuantity(int id, string color, int quantity)
        {
            var cart = Session["Bloomfiy.Cart"] as List<CartItem> ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == id && x.Color == color);

            if (item != null)
            {
                if (quantity < 1)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;
            }

            Session["Bloomfiy.Cart"] = cart;

            return Json(new
            {
                itemTotal = item != null ? item.TotalPrice : 0,
                cartTotal = cart.Sum(x => x.TotalPrice)
            });
        }

        [HttpGet]
        public ActionResult GetCartJson()
        {
            var cart = Session["Bloomfiy.Cart"] as List<CartItem> ?? new List<CartItem>();
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RestoreFromLocalStorage(List<CartItem> cart)
        {
            if (cart != null && cart.Any())
            {
                Session["Bloomfiy.Cart"] = cart;
            }
            return new HttpStatusCodeResult(200);
        }

    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bloomfiy_final.Models;
using System.Data.Entity;

namespace Bloomfiy_final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.TotalOrders = db.Orders.Count();
            ViewBag.PendingOrders = db.Orders.Count(o => o.Status == "Pending");
            ViewBag.DeliveredOrders = db.Orders.Count(o => o.Status == "Delivered");
            ViewBag.TotalProducts = db.Products.Count();
            ViewBag.TotalUsers = db.Users.Count();

            return View();
        }

    }
}

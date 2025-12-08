using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy_final.Controllers
{
    public class InformationController : Controller
    {
        public ActionResult FAQ() => View();
        public ActionResult DeliveryInfo() => View();
        public ActionResult PrivacyPolicy() => View();
        public ActionResult TermsOfService() => View();
        public ActionResult RefundPolicy() => View();
    }
}
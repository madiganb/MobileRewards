using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile_Shared_Web.Controllers
{
    public class AwardItemController : BaseController
    {
        // GET: AwardItem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkAsFraud(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkAsError(string id)
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace Mobile_Shared_Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}
using System.Web.Mvc;

namespace iasset.FlightDashBoard.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}
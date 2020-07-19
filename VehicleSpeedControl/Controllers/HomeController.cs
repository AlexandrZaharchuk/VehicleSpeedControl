using System.Web.Mvc;

namespace VehicleSpeedControl.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Система учета и контроля скоростного режима транспортных средств.";

			return View();
		}
	}
}

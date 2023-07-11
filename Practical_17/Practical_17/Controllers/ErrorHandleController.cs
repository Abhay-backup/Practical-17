using Microsoft.AspNetCore.Mvc;

namespace Practical_17.Controllers
{
	[Route("Error/{action}")]
	public class ErrorHandleController : Controller
	{
		public IActionResult NotFound()
		{
			return View();
		}
	}
}

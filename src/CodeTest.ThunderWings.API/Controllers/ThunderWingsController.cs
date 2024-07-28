using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Services;

using Microsoft.AspNetCore.Mvc;

namespace CodeTest.ThunderWings.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ThunderWingsController(ILogger<ThunderWingsController> logger, IThunderWingService service) : ControllerBase
	{
		[HttpGet()]
		public IActionResult FindAll()
		{
			var result = service.FindAll();
			return Ok(result);
		}
	}
}
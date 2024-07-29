using System.Text.Json;

using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Paging;
using CodeTest.ThunderWings.Data.Services;

using Microsoft.AspNetCore.Mvc;

namespace CodeTest.ThunderWings.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[ProducesResponseType<Aircraft>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class ThunderWingsController(ILogger<ThunderWingsController> logger, IThunderWingService service) : ControllerBase
	{
		[HttpPost("FindAll")]
		public IActionResult Find([FromBody] AircraftFilter aircraftFilter)
		{
			logger.LogInformation("CodeTest.ThunderWings.API.Controllers.SalesController.FindAll");
			var result = service.Find(aircraftFilter);
			Response.Headers.Append("X-Pagination", JsonSerializer.Serialize((IPagedList)result));
			return Ok(result);
		}

		[HttpPost("Reset/ResetDataFile")]
		public IActionResult ResetData()
		{
			logger.LogInformation("CodeTest.ThunderWings.API.Controllers.SalesController.ResetData");
			service.ResetData();
			return Ok();
		}
	}
}
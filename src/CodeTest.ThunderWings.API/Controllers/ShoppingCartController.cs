using System.Text.Json;

using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Services;

using Microsoft.AspNetCore.Mvc;

namespace CodeTest.ThunderWings.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ProducesResponseType<Aircraft>(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartService service) : ControllerBase
	{
		[HttpPost("add")]
		public IActionResult AddToShoppingCart([FromBody] ShoppingCartItem item)
		{
			logger.LogInformation("CodeTest.ThunderWings.API.Controllers.SalesController.AddToShoppingCart");
			var result = service.Add(item);

			if (result.IsSuccess && result.Value != null)
				return Ok(result.Value);
			else if (result.IsSuccess)
				return Ok(result.Message);
			else
				return BadRequest(result.Message);
		}

		[HttpPost("find")]
		public IActionResult AddToShoppingCart([FromBody] ShoppingCartItemFilter filter)
		{
			logger.LogInformation("CodeTest.ThunderWings.API.Controllers.SalesController.FindAll");
			var result = service.Find(filter);
			Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result));
			return Ok(result);
		}

		[HttpDelete("Delete")]
		public IActionResult RemoveShoppingCart([FromBody] ShoppingCartItem item)
		{
			logger.LogInformation("CodeTest.ThunderWings.API.Controllers.SalesController.RemoveShoppingCart");
			var result = service.Remove(item);
			return Ok(result);
		}
	}
}
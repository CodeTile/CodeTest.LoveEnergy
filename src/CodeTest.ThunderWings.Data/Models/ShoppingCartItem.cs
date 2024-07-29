using System.Diagnostics.CodeAnalysis;

namespace CodeTest.ThunderWings.Data.Models
{
	[GenerateAutoFilter]
	[ExcludeFromCodeCoverage]
	public class ShoppingCartItem
	{
		public string? Name { get; set; }
		public int Quantity { get; set; }
		public string? ShopperId { get; set; }
	}
}
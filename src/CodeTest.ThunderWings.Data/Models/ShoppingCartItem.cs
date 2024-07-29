namespace CodeTest.ThunderWings.Data.Models
{
	[GenerateAutoFilter]
	public class ShoppingCartItem
	{
		public string? Name { get; set; }
		public int Quantity { get; set; }
		public string? ShopperId { get; set; }
	}
}
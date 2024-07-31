using CodeTest.ThunderWings.Data.Models;

namespace CodeTest.ThunderWings.Data.Tests.TestData
{
	internal static class ShoppingCartItemTestData
	{
		public static List<ShoppingCartItem> ShoppingCartItems =
		[
			new ()
			{
				Name = "Mitsubishi F-2",
				Quantity = 6,
				ShopperId = "The Red Baron",
			},

			new ()
			{
				Name = "Northrop F-20 Tigershark",
				Quantity = 1,
				ShopperId = "The Red Baron",
			},

			new ()
			{
				Name = "Chengdu J-9",
				Quantity = 62,
				ShopperId = "The Red Baron",
			},

			new ()
			{
				Name = "Chengdu J-9",
				Quantity = 62,
				ShopperId = "Darth Vader",
			}
		];
	}
}
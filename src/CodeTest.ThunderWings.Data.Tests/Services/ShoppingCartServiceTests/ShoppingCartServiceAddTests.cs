using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Services;

using FluentAssertions;

using Microsoft.Extensions.Configuration;

namespace CodeTest.ThunderWings.Data.Tests.Services.ShoppingCartServiceTests
{
	[TestClass]
	public class ShoppingCartServiceAddTests
	{
		private IThunderWingService _thunderWingService;
		private IConfiguration mockConfiguration;

		[TestMethod]
		public void AddDuplicate()
		{
			// arrange
			var uot = new ShoppingCartService(mockConfiguration, _thunderWingService);
			var shoppingCartItem = new ShoppingCartItem()
			{
				Name = "Mitsubishi F-2",
				Quantity = 3,
				ShopperId = "The Red Baron",
			};
			uot.Add(shoppingCartItem);
			// act
			uot.Add(shoppingCartItem);
			// assert
			var actual = uot.Find(new ShoppingCartItemFilter() { }).AsQueryable();
			actual.Count().Should().Be(1);
			actual.First().Should().BeEquivalentTo(new ShoppingCartItem()
			{
				Name = "Mitsubishi F-2",
				Quantity = 6,
				ShopperId = "The Red Baron",
			}
			);
		}

		[TestMethod]
		public void AddGood()
		{
			// arrange
			var uot = new ShoppingCartService(mockConfiguration, _thunderWingService);
			var shoppingCartItem = new ShoppingCartItem()
			{
				Name = "Mitsubishi F-2",
				Quantity = 3,
				ShopperId = "The Red Baron",
			};
			// act
			uot.Add(shoppingCartItem);
			// assert
			var actual = uot.Find(new ShoppingCartItemFilter() { }).AsQueryable();
			actual.Count().Should().Be(1);
			actual.First().Should().BeEquivalentTo(shoppingCartItem);
		}

		[TestMethod]
		public void AddInvalidName()
		{
			// arrange
			var uot = new ShoppingCartService(mockConfiguration, _thunderWingService);
			var shoppingCartItem = new ShoppingCartItem()
			{
				Name = "X Wing",
				Quantity = 3,
				ShopperId = "The Red Baron",
			};
			// act
			uot.Add(shoppingCartItem);
			var actual = uot.Find(new ShoppingCartItemFilter() { }).AsQueryable();
			// assert
			actual.Count().Should().Be(0);
		}

		[TestInitialize]
		public void Setup()
		{
			// mock AppConfig
			var inMemorySettings = new Dictionary<string, string> {
					{"Files:Original", "{{sln}}\\Data\\Aircraft.json"},
					{"Files:Active", "Data\\Aircraft.json"},
					{"Files:ShoppingCart", "Data\\ShoppingCartServiceTests\\ShoppingCart.json"},
				};
			mockConfiguration = new ConfigurationBuilder()
				.AddInMemoryCollection(inMemorySettings)
				.Build();
			_thunderWingService = new ThunderWingService(mockConfiguration);
			_thunderWingService.ResetData();
		}
	}
}
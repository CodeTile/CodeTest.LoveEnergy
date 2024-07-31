using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Services;
using CodeTest.ThunderWings.Data.Tests.TestData;

using FluentAssertions;

using Microsoft.Extensions.Configuration;

namespace CodeTest.ThunderWings.Data.Tests.Services.ShoppingCartServiceTests
{
	[TestClass]
	public class ShoppingCartServiceRemoveTests
	{
		private IThunderWingService _thunderWingService;
		private IConfiguration mockConfiguration;

		[TestMethod]
		public void RemoveEmpty()
		{
			// arrange
			var uot = new ShoppingCartService(mockConfiguration, _thunderWingService);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[0]);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[1]);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[2]);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[3]);

			// act
			uot.Remove(new ShoppingCartItem());
			// assert
			var actual = uot.Find(new ShoppingCartItemFilter() { }).AsQueryable();
			actual.Count().Should().Be(4);
			actual.Should().BeEquivalentTo(ShoppingCartItemTestData.ShoppingCartItems.AsQueryable());
		}

		[TestMethod]
		public void RemoveTigerShark()
		{
			// arrange
			var uot = new ShoppingCartService(mockConfiguration, _thunderWingService);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[0]);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[1]);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[2]);
			uot.Add(ShoppingCartItemTestData.ShoppingCartItems[3]);
			// act
			uot.Remove(ShoppingCartItemTestData.ShoppingCartItems.Single(m => m.Name == "Northrop F-20 Tigershark"));
			// assert
			var actual = uot.Find(new ShoppingCartItemFilter() { }).AsQueryable();
			actual.Count().Should().Be(3);
			actual.Should().BeEquivalentTo(
				ShoppingCartItemTestData.ShoppingCartItems.Where(m => m.Name != "Northrop F-20 Tigershark").AsQueryable()
			);
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
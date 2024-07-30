using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Services;
using CodeTest.ThunderWings.Data.Tests.TestData;

using FluentAssertions;

namespace CodeTest.ThunderWings.Data.Tests.Services
{
	[TestClass]
	public class ThunderWingServiceTests
	{
		[TestMethod]
		public void Find01()
		{
			// arrange
			var expected = AircraftTestData.Aircraft09.AsQueryable();
			var uot = new ThunderWingService(null);
			uot._data = AircraftTestData.Aircraft09.AsQueryable();
			var filter = new AircraftFilter();
			//act
			var actual = uot.Find(filter);
			//assert
			actual.TotalCount.Should().Be(9);
			actual.PageSize.Should().Be(10);
			actual.TotalPages.Should().Be(1);
			actual.CurrentPage.Should().Be(1);
			actual.AsQueryable().Should().BeEquivalentTo(expected);
		}
	}
}
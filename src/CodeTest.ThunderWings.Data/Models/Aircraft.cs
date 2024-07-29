using System.Diagnostics.CodeAnalysis;

namespace CodeTest.ThunderWings.Data.Models
{
	[GenerateAutoFilter]
	[ExcludeFromCodeCoverage]
	public class Aircraft
	{
		public string? Country { get; set; }
		public string? Manufacturer { get; set; }
		public string? Name { get; set; }
		public int Price { get; set; }
		public string? Role { get; set; }
		public int TopSpeed { get; set; }
	}
}
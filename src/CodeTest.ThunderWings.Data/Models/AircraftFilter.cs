namespace CodeTest.ThunderWings.Data.Models
{
	/// <summary>
	/// https://enisn-projects.io/docs/en/AutoFilterer/latest
	/// </summary>
	public partial class AircraftFilter
	{
		public void Clean()
		{
			if (Sort == "string")
				Sort = string.Empty;
			if (Country == "string")
				Country = string.Empty;
			if (Manufacturer == "string")
				Manufacturer = string.Empty;
			if (Name == "string")
				Name = string.Empty;
			if (Role == "string")
				Role = string.Empty;
			if (Page < 1)
				Page = 1;
			if (PerPage < 1)
				PerPage = 50;
			if (Price.Min < 0)
				Price.Min = 0;
			if (Price.Max < 1)
				Price.Max = Int32.MaxValue;
			if (TopSpeed.Min < 0)
				TopSpeed.Min = 0;
			if (TopSpeed.Max < 1)
				TopSpeed.Max = Int32.MaxValue;
		}
	}
}
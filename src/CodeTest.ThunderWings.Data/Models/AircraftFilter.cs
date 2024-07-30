namespace CodeTest.ThunderWings.Data.Models
{
	/// <summary>
	/// https://enisn-projects.io/docs/en/AutoFilterer/latest
	/// </summary>
	public partial class AircraftFilter
	{
		private const string WordString = "string";

		public void Clean()
		{
			if (Sort == WordString)
				Sort = string.Empty;
			if (Country == WordString)
				Country = string.Empty;
			if (Manufacturer == WordString)
				Manufacturer = string.Empty;
			if (Name == WordString)
				Name = string.Empty;
			if (Role == WordString)
				Role = string.Empty;
			if (Page < 1)
				Page = 1;
			if (PerPage < 1)
				PerPage = 50;
			if (Price != null)
			{
				if (Price.Min < 0)
					Price.Min = 0;
				if (Price.Max < 1)
					Price.Max = Int32.MaxValue;
			}
			if (TopSpeed != null)
			{
				if (TopSpeed.Min < 0)
					TopSpeed.Min = 0;
				if (TopSpeed.Max < 1)
					TopSpeed.Max = Int32.MaxValue;
			}
		}
	}
}
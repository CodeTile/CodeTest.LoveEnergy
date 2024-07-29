namespace CodeTest.ThunderWings.Data.Models
{
	/// <summary>
	/// https://enisn-projects.io/docs/en/AutoFilterer/latest
	/// </summary>
	public partial class ShoppingCartItemFilter
	{
		public void Clean()
		{
			if (Sort == "string")
				Sort = string.Empty;
			if (ShopperId == "string")
				ShopperId = string.Empty;
			if (Name == "string")
				Name = string.Empty;

			if (Page < 1)
				Page = 1;
			if (PerPage < 1)
				PerPage = 50;

			if (Quantity.Min < 0)
				Quantity.Min = 0;
			if (Quantity.Max < 1)
				Quantity.Max = Int32.MaxValue;
		}
	}
}
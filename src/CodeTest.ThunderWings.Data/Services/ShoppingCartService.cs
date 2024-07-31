using System.Text.Json;

using AutoFilterer.Extensions;

using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Paging;
using CodeTest.ThunderWings.Data.Validatiors;

using Microsoft.Extensions.Configuration;

namespace CodeTest.ThunderWings.Data.Services
{
	public interface IShoppingCartService
	{
		Result<ShoppingCartItem> Add(ShoppingCartItem item);

		PagedList<ShoppingCartItem> Find(ShoppingCartItemFilter filter);

		bool Remove(ShoppingCartItem item);
	}

	public class ShoppingCartService : IShoppingCartService
	{
		public ShoppingCartService(IConfiguration configuration, IThunderWingService thunderWingService)
		{
			this.Configuration = configuration;
			this._thunderWingService = thunderWingService;
			LoadData();
		}

		internal IQueryable<ShoppingCartItem> _data = Enumerable.Empty<ShoppingCartItem>().AsQueryable();

		private readonly JsonSerializerOptions _serialisationOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		private readonly IThunderWingService _thunderWingService;
		private readonly IConfiguration Configuration;

		public Result<ShoppingCartItem> Add(ShoppingCartItem item)
		{
			var validator = new ShoppingCartItemValidator();
			var validationResult = validator.Validate(item);
			if (!validationResult.IsValid)
			{
				string allMessages = validationResult.ToString("~");
				return Result<ShoppingCartItem>.Failure(allMessages);
			}
			//Check if plane exists in inventory
			var aircraft = _thunderWingService.Find(new AircraftFilter() { Name = item.Name });
			if (aircraft == null || aircraft.Count == 0)
				return Result<ShoppingCartItem>.Failure($"The aircraft '{item.Name}' is not available!");

			var existing = _data.SingleOrDefault(m => m.ShopperId!.Equals(item.ShopperId, StringComparison.CurrentCultureIgnoreCase) && m.Name!.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase));
			if (existing != null)
			{
				item.Quantity += existing.Quantity;
				Remove(existing);
			}

			_data = _data!.Union([item]);
			SaveData();
			return Result<ShoppingCartItem>.Success(item);
		}

		public PagedList<ShoppingCartItem> Find(ShoppingCartItemFilter filter)
		{
			if (_data == null)
				LoadData();
			filter.Clean();
			var data = _data.ApplyFilterWithoutPagination(filter);
			return PagedList<ShoppingCartItem>.Create(data, filter.Page, filter.PerPage);
		}

		public bool Remove(ShoppingCartItem item)
		{
			// There are probably better ways to do this.
			_data = _data
				.Where(m => !(m.ShopperId.Equals(item.ShopperId, StringComparison.CurrentCultureIgnoreCase) && m.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)))
				.AsQueryable();
			SaveData();
			return true;
		}

		private void LoadData()
		{
			var ShoppingCartFileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration["Files:ShoppingCart"]!));
			if (!ShoppingCartFileInfo.Exists)
			{
				_data = Enumerable.Empty<ShoppingCartItem>().AsQueryable();
				return;
			}
			var fileContents = File.ReadAllText(ShoppingCartFileInfo.FullName);
			_data = (JsonSerializer.Deserialize<IEnumerable<ShoppingCartItem>>(fileContents, _serialisationOptions)
				?? []
				).AsQueryable();
		}

		private void SaveData()
		{
			var ShoppingCartFileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration["Files:ShoppingCart"]!));
			var jsonString = JsonSerializer.Serialize(_data, _serialisationOptions);
			File.WriteAllText(ShoppingCartFileInfo.FullName, jsonString);
		}
	}
}
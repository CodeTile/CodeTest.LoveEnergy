using System.Text.Json;

using AutoFilterer.Extensions;

using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Paging;

using Microsoft.Extensions.Configuration;

namespace CodeTest.ThunderWings.Data.Services
{
	public interface IShoppingCartService
	{
		ShoppingCartItem Add(ShoppingCartItem item);

		PagedList<ShoppingCartItem> Find(ShoppingCartItemFilter filter);

		bool Remove(ShoppingCartItem item);
	}

	public class ShoppingCartService : IShoppingCartService
	{
		public ShoppingCartService(IConfiguration configuration)
		{
			this.Configuration = configuration;
			LoadData();
		}

		internal IQueryable<ShoppingCartItem> _data = Enumerable.Empty<ShoppingCartItem>().AsQueryable();

		private readonly JsonSerializerOptions _serialisationOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		private readonly IConfiguration Configuration;

		public ShoppingCartItem Add(ShoppingCartItem item)
		{
			var existing = _data.SingleOrDefault(m => m.ShopperId!.Equals(item.ShopperId, StringComparison.CurrentCultureIgnoreCase) && m.Name!.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase));
			if (existing != null)
			{
				item.Quantity += existing.Quantity;
				Remove(existing);
			}

			//var local = (new List<ShoppingCartItem>() { item }).AsQueryable();
			_data = _data!.Union([item]);
			SaveData();
			return item;
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
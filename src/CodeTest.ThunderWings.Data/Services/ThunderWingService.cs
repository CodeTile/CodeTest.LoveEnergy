using System.Text.Json;

using AutoFilterer.Extensions;

using CodeTest.ThunderWings.Data.Helpers;
using CodeTest.ThunderWings.Data.Models;
using CodeTest.ThunderWings.Data.Paging;

using Microsoft.Extensions.Configuration;

namespace CodeTest.ThunderWings.Data.Services
{
	public interface IThunderWingService
	{
		PagedList<Aircraft> Find(AircraftFilter aircraftFilter);

		void ResetData();
	}

	public class ThunderWingService(IConfiguration configuration) : IThunderWingService
	{
		internal IQueryable<Aircraft>? _data = null;

		private readonly JsonSerializerOptions _serialisationOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		public PagedList<Aircraft> Find(AircraftFilter aircraftFilter)
		{
			if (_data == null)
				LoadData();
			aircraftFilter.Clean();
			var data = _data.ApplyFilterWithoutPagination(aircraftFilter);
			return PagedList<Aircraft>.Create(data, aircraftFilter.Page, aircraftFilter.PerPage);
		}

		public void LoadData()
		{
			var workingCopy = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration["Files:Active"]!));
			if (!workingCopy.Exists)
				ResetData();
			var fileContents = File.ReadAllText(workingCopy.FullName);
			_data = (JsonSerializer.Deserialize<IEnumerable<Aircraft>>(fileContents, _serialisationOptions)
				?? []
				).AsQueryable();
		}

		/// <summary>
		/// Create a new copy of the data file. This would not be included in production code.
		/// </summary>
		public void ResetData()
		{
			var originalDataFile = Helper.IO.TryGetFileInfo(configuration["Files:Original"]!);
			var fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration["Files:Active"]!));

			if (fi.Directory!.Exists)
				fi.Directory.Delete(true);
			fi.Directory.Create();

			originalDataFile!.CopyTo(fi.FullName, true);
		}
	}
}
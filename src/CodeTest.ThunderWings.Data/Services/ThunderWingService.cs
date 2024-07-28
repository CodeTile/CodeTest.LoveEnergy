using System.Text.Json;

using CodeTest.ThunderWings.Data.Helpers;
using CodeTest.ThunderWings.Data.Models;

using Microsoft.Extensions.Configuration;

namespace CodeTest.ThunderWings.Data.Services
{
	public interface IThunderWingService
	{
		IQueryable<Aircraft> FindAll();
	}

	public class ThunderWingService(IConfiguration configuration) : IThunderWingService
	{
		internal IQueryable<Aircraft>? _data = null;

		private JsonSerializerOptions _serialisationOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		public IQueryable<Aircraft> FindAll()
		{
			ResetDataFile();
			if (_data == null)
				LoadData();
			return _data ?? Enumerable.Empty<Aircraft>().AsQueryable();
		}

		public void LoadData()
		{
			var workingCopy = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration["Files:Active"]!));
			if (!workingCopy.Exists)
				ResetDataFile();
			var fileContents = File.ReadAllText(workingCopy.FullName);
			_data = JsonSerializer.Deserialize<IEnumerable<Aircraft>>(fileContents, _serialisationOptions)!.AsQueryable();
		}

		/// <summary>
		/// Create a new copy of the data file.
		/// </summary>
		public void ResetDataFile()
		{
			var originalDataFile = Helper.IO.TryGetFileInfo(configuration["Files:Original"]!);
			var fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration["Files:Active"]!));

			if (!fi.Directory!.Exists)
				fi.Directory.Create();

			originalDataFile!.CopyTo(fi.FullName, true);
		}
	}
}
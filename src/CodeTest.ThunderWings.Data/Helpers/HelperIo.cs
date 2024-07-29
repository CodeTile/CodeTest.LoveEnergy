using System.Diagnostics.CodeAnalysis;

namespace CodeTest.ThunderWings.Data.Helpers
{
	[ExcludeFromCodeCoverage]
	internal static partial class Helper
	{
		internal static class IO
		{
			internal static FileInfo? TryGetFileInfo(string relativePathToSolution)
			{
				var rootPath = TryGetSolutionDirectoryInfo()!.FullName;
				var filepath = relativePathToSolution.Replace("{{sln}}", rootPath);

				return new FileInfo(filepath);
			}

			internal static DirectoryInfo? TryGetSolutionDirectoryInfo()
			{ return TryGetSolutionDirectoryInfo(currentPath: string.Empty); }

			internal static DirectoryInfo? TryGetSolutionDirectoryInfo(string currentPath)
			{
				var directory = new DirectoryInfo(
					!string.IsNullOrEmpty(currentPath) ? currentPath : Directory.GetCurrentDirectory());
				while (directory != null && !directory.GetFiles("*.sln").Any())
				{
					directory = directory.Parent;
				}
				return directory;
			}
		}
	}
}
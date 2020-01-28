using System.IO;

namespace ExamplesGenerator
{
	public static class ExampleLocation
	{
		private static string _root;

		public static DirectoryInfo ExamplesAsciiDocDir { get; } = new DirectoryInfo($@"{Root}../../examples");
		public static DirectoryInfo ExamplesCSharpProject { get; } = new DirectoryInfo($@"{Root}../../tests/Examples");

		private static string Root
		{
			get
			{
				if (_root != null) return _root;

				var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

				var runningAsNetCore =
					directoryInfo.Name == "ExamplesGenerator" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "src";

				_root = runningAsNetCore ? "" : @"../../../";
				return _root;
			}
		}
	}
}

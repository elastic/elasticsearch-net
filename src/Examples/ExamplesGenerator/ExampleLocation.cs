using System.IO;

namespace ExamplesGenerator
{
	public static class ExampleLocation
	{
		private static string _root;
		public static string ExamplesDir { get; } = $@"{Root}../../../src/Examples/Examples";

		public static string Root
		{
			get
			{
				if (_root != null) return _root;

				var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

				var runningAsNetCore =
					directoryInfo.Name == "ExamplesGenerator" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "Examples";

				_root = runningAsNetCore ? "" : @"../../../";
				return _root;
			}
		}
	}
}

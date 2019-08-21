using System.IO;
using System.Reflection;

namespace Tests.Yaml
{
	public static class GeneratorLocations
	{
		private static string _root;
		/// <summary>
		/// Points to the root of the project whether it's run through the editor or the .NET cli tool.
		/// </summary>
		public static string Root
		{
			get
			{
				if (_root != null) return _root;

				var pwd = Directory.GetCurrentDirectory();
				var directoryInfo = new DirectoryInfo(pwd);

				var runningAsDnx =
					directoryInfo.Name == "Tests.Yaml" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "Tests";

				var relative =  runningAsDnx ? "" : @"../../../";
				var fullPath = Path.Combine(pwd, relative);
				_root = Path.GetFullPath(fullPath);
				return _root;
			}
		}
	}
}

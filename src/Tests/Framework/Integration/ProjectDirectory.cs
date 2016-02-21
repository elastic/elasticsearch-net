using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Framework.Integration
{
    public static class ProjectDirectory
    {
		static ProjectDirectory()
		{
			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			// If running the classic .NET solution, tests run from bin/{config} directory,
			// but when running DNX solution, tests run from the test project root
			Root = directoryInfo.Name == "Tests" &&
										directoryInfo.Parent != null &&
										directoryInfo.Parent.Name == "src"
				? @".\"
				: @"..\..\";
		}

		public static string Root { get; }
    }
}

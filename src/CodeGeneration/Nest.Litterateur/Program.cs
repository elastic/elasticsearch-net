using Nest.Litterateur.Documentation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.Litterateur
{
	class Program
	{
		static void Main(string[] args) =>
			LitUp.Go();

		public static class LitUp
		{
			private readonly static string _testFolder = @"..\..\..\..\..\src\Tests";
			private readonly static string _docFolder = @"..\..\..\..\..\docs\contents\new";
			private static readonly string[] _skipFolders = new[] { "Nest.Tests.Literate", "Debug", "Release" };

			public static IEnumerable<DocumentationFile> FindAll()
			{
				var csfiles = Directory.GetFiles(_testFolder, "*.cs", SearchOption.AllDirectories);
				foreach (var csfile in csfiles)
				{
					var dirInfo = new DirectoryInfo(csfile);
					var fileInfo = new FileInfo(csfile);
					if (_skipFolders.Contains(dirInfo.Parent.Name))
						continue;

					yield return DocumentationFile.Load(csfile);
				}
			}

			public static void Go()
			{
				var files = FindAll();
				foreach(var file in files)
				{
					var path = Regex.Replace(file.FileName, @"(^.+\\Tests\\|\.cs$)", "") + ".md";
					var docFileName = Path.GetFullPath(Path.Combine(_docFolder, path));
					var fileInfo = new FileInfo(docFileName);
					Directory.CreateDirectory(fileInfo.Directory.FullName);
					File.WriteAllText(docFileName, file.Body);
				}

			}

				
		}
	}
}
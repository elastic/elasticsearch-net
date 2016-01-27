using System.IO;
using Nest.Litterateur.Documentation;

namespace Nest.Litterateur
{
	public static class Program
	{
		private static string DefaultTestFolder;
		private static string DefaultDocFolder;

		static Program()
		{
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
			if (currentDirectory.Name == "Nest.Litterateur" && currentDirectory.Parent.Name == "CodeGeneration")
			{
				DefaultTestFolder = @"..\..\Tests";
				DefaultDocFolder = @"..\..\..\docs\asciidoc";
			}
			else
			{
				DefaultTestFolder = @"..\..\..\..\..\src\Tests";
				DefaultDocFolder = @"..\..\..\..\..\docs\asciidoc";
			}
		}

		public static string InputFolder => DefaultTestFolder;

		
		public static string OutputFolder => DefaultDocFolder;

		static void Main(string[] args) => LitUp.Go(args);
	}
}
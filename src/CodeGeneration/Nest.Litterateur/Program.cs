using System.IO;

namespace Nest.Litterateur
{
	public static class Program
	{
		public const string ImagesDir = "images";

		static Program()
		{
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
			if (currentDirectory.Name == "Nest.Litterateur" && currentDirectory.Parent.Name == "CodeGeneration")
			{
				InputDirPath = @"..\..\Tests";
				OutputDirPath = @"..\..\..\docs\asciidoc";
			}
			else
			{
				InputDirPath = @"..\..\..\..\..\src\Tests";
				OutputDirPath = @"..\..\..\..\..\docs\asciidoc";
			}
		}

		public static string ImagesDirPath => Path.Combine(OutputDirPath, ImagesDir);

		public static string InputDirPath { get; }

		public static string OutputDirPath { get; }

		static void Main(string[] args) => LitUp.Go(args);
	}
}
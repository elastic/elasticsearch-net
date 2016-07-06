using System.IO;

namespace DocGenerator
{
	public static class Program
	{
		static Program()
		{
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
			if (currentDirectory.Name == "DocGenerator" && currentDirectory.Parent.Name == "CodeGeneration")
			{
				InputDirPath = @"..\..\Tests";
				OutputDirPath = @"..\..\..\docs";
			}
			else
			{
				InputDirPath = @"..\..\..\..\..\..\src\Tests";
				OutputDirPath = @"..\..\..\..\..\..\docs";
			}
		}

		public static string InputDirPath { get; }

		public static string OutputDirPath { get; }

		static void Main(string[] args) => LitUp.Go(args);
	}
}

using System;
using System.IO;

namespace DocGenerator
{
	public static class Program
	{
		static Program()
		{
			string P(string path)
			{
				return path.Replace(@"\", Path.DirectorySeparatorChar.ToString());
			}
			
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            if (currentDirectory.Name == "DocGenerator" && currentDirectory.Parent.Name == "CodeGeneration")
			{
				Console.WriteLine("IDE: " + currentDirectory);
				
                InputDirPath = P(@"..\..\");
				OutputDirPath = P(@"..\..\..\docs");
                BuildOutputPath = P(@"..\..\..\build\output");
			}
			else
			{
				Console.WriteLine("CMD: " + currentDirectory);
				InputDirPath = P(@"..\..\..\..\src");
				OutputDirPath = P(@"..\..\..\..\docs");
                BuildOutputPath = P(@"..\..\..\..\build\output");
			}
        }

        public static string BuildOutputPath { get; }

		public static string InputDirPath { get; }

		public static string OutputDirPath { get; }

		static int Main(string[] args)
		{
		    try
		    {
                LitUp.GoAsync(args).Wait();
			    return 0;
		    }
		    catch (AggregateException ae)
		    {
			    var ex = ae.InnerException ?? ae;
                Console.WriteLine(ex.Message);
			    return 1;
		    }
		}
	}
}

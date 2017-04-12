using System;
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
                InputDirPath = @"..\..\";
				OutputDirPath = @"..\..\..\docs";
                BuildOutputPath = @"..\..\..\build\output";
			}
			else
			{
				InputDirPath = @"..\..\..\..\..\src";
				OutputDirPath = @"..\..\..\..\..\docs";
                BuildOutputPath = @"..\..\..\..\..\build\output";
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

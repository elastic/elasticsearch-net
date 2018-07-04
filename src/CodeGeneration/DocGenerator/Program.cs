using System;
using System.Diagnostics;
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
                BuildOutputPath = P(@"..\..\..\src");
			}
			else
			{
				Console.WriteLine("CMD: " + currentDirectory);
				InputDirPath = P(@"..\..\..\..\src");
				OutputDirPath = P(@"..\..\..\..\docs");
                BuildOutputPath = P(@"..\..\..\..\build\output");
			}

			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					FileName = "git.exe",
					CreateNoWindow = true,
					WorkingDirectory = Environment.CurrentDirectory,
					Arguments = "rev-parse --abbrev-ref HEAD"
				}
			};

			try
			{
				process.Start();
				BranchName = process.StandardOutput.ReadToEnd().Trim();
				process.WaitForExit();
			}
			catch (Exception)
			{
				BranchName = "master";
			}
			finally
			{
				process.Dispose();
			}
        }

        public static string BuildOutputPath { get; }

		public static string InputDirPath { get; }

		public static string OutputDirPath { get; }

		/// <summary>
		/// The branch name to include in generated docs to link back to the original source file
		/// </summary>
		public static string BranchName { get; set; }

		/// <summary>
		/// The Elasticsearch documentation version to link to
		/// </summary>
		public static string DocVersion => "6.2";

		private static int Main(string[] args)
		{
		    try
		    {
			    if (args.Length > 0)
				    BranchName = args[0];

			    Console.WriteLine($"Using branch name {BranchName} in documentation");

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



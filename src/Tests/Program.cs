using System;
using System.Linq;

namespace Tests
{
	// Microsoft test sdk injects a Main method for some reason into program
	// (Relates to this issue: https://youtrack.jetbrains.com/issue/RSRP-464233)
	// (See also https://github.com/elastic/elasticsearch-net/pull/2793)
	// We provide an alternative StartupObject as part of the csproj for e.g `dotnet run` or executable output
	// That will run the benchmarking/profiling.
	public class Program { }

	public static class ProgramDispatcher
	{
		public static int Main(string[] args)
		{
			if (args.Length == 0)
				Console.WriteLine("Must atleast specify one argument to indicate what command to run");

			var command = args[0].ToLowerInvariant();
			var programArguments = args.Skip(1).ToArray();
			switch (command)
			{
				case "profile": return ProfileProgram.Main(programArguments);
				case "benchmark": return BenchmarkProgram.Main(programArguments);
				case "cluster": return ClusterLaunchProgram.Main(programArguments);
				default:
					Console.Error.WriteLine($"Unknown command '{command}");
					return 1;
			}
		}
	}
}

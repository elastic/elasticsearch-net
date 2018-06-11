using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FluentAssertions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.Profiling;
using Tests.Framework.Profiling.Memory;
using Tests.Framework.Profiling.Performance;
using Tests.Framework.Profiling.Timeline;

namespace Tests
{
	// Microsoft test sdk injects a Main method for some reason into program
	// (Relates to this issue: https://youtrack.jetbrains.com/issue/RSRP-464233)
	// (See also https://github.com/elastic/elasticsearch-net/pull/2793)
	// We provide an alternative StartupObject as part of the csproj for e.g `dotnet run` or executable output
	// That will run the benchmarking/profiling.public class Program{ }
	public class Program { }

	public class BenchmarkProgram
	{
		static BenchmarkProgram()
		{
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
			if ((currentDirectory.Name == "Debug" || currentDirectory.Name == "Release") && currentDirectory.Parent.Name == "bin")
			{
				SdkPath = new DirectoryInfo(
					Path.Combine(
						Directory.GetCurrentDirectory(),
						$@".\..\..\..\..\build\tools\{SelfProfileSdkDirectory}")).FullName;

				OutputPath = new DirectoryInfo(
					Path.Combine(
						Directory.GetCurrentDirectory(),
						$@".\..\..\..\..\build\output\profiling")).FullName;
			}
			else
			{
				SdkPath = new DirectoryInfo(
					Path.Combine(
						Directory.GetCurrentDirectory(),
						$@".\build\tools\{SelfProfileSdkDirectory}")).FullName;

				OutputPath = new DirectoryInfo(
					Path.Combine(
						Directory.GetCurrentDirectory(),
						$@".\build\output\profiling")).FullName;
			}
		}

		public static string InputDirPath { get; }

		public static string OutputDirPath { get; }

		private const string SelfProfileSdkDirectory = "dottrace-selfprofile";

		private static string SdkPath { get; }
		private static string OutputPath { get; }

		public static void Main(string[] args)
		{
			if (args.Length == 0)
				Console.WriteLine("Must specify at least one argument: Profile/Benchmark");

			var arguments = args.Skip(1).ToArray();
			if (args[0].Equals("Profile", StringComparison.OrdinalIgnoreCase))
			{
#if FEATURE_PROFILING
				var configuration = ProfileConfiguration.Parse(arguments);
				Console.WriteLine("Running Profiling with the following:");
				Console.WriteLine($"- SdkPath: {SdkPath}");
				Console.WriteLine($"- OutputPath: {OutputPath}");
				Console.WriteLine($"- Classes: [{(configuration.ClassNames.Any() ? string.Join(",", configuration.ClassNames) : "*All*")}]");

				using (var cluster = new ProfilingCluster())
				{
					foreach (var profilingFactory in CreateProfilingFactory(cluster))
					{
						profilingFactory.Run(configuration);
						profilingFactory.RunAsync(configuration).Wait();
					}
				}
#else
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Tests.exe is not built with Profiling support");
				Console.ResetColor();
				Environment.Exit(9);
#endif
			}
			else if (args[0].Equals("Benchmark", StringComparison.OrdinalIgnoreCase))
			{
				Console.WriteLine("Running Benchmarking.");
				if (args.Count() > 1 && args[1].Equals("non-interactive", StringComparison.OrdinalIgnoreCase))
				{
					Console.WriteLine("Running in Non-Interactive mode.");
					foreach (var benchmarkType in GetBenchmarkTypes())
					{
						BenchmarkRunner.Run(benchmarkType);
					}
					return;
				}

				Console.WriteLine("Running in Interactive mode.");
				var benchmarkSwitcher = new BenchmarkSwitcher(GetBenchmarkTypes());
				benchmarkSwitcher.Run(arguments);
			}
		}

#if FEATURE_PROFILING
		private static IEnumerable<IProfileFactory> CreateProfilingFactory(ProfilingCluster cluster)
		{
			yield return new PerformanceProfileFactory(SdkPath, OutputPath, cluster, Assembly.GetEntryAssembly(), new ColoredConsoleWriter());
			yield return new TimelineProfileFactory(SdkPath, OutputPath, cluster, Assembly.GetEntryAssembly(), new ColoredConsoleWriter());
			yield return new MemoryProfileFactory(SdkPath, OutputPath, cluster, Assembly.GetEntryAssembly(), new ColoredConsoleWriter());
		}
#endif
		private static Type[] GetBenchmarkTypes()
		{
			IEnumerable<Type> types;

			try
			{
				types = typeof(Program).Assembly().GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				types = e.Types.Where(t => t != null);
			}

			return types
				.Where(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
							 .Any(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Any()))
				.OrderBy(t => t.Namespace)
				.ThenBy(t => t.Name)
				.ToArray();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tests.Framework.Profiling;
using Tests.Framework.Profiling.Memory;
using Tests.Framework.Profiling.Performance;
using Tests.Framework.Profiling.Timeline;

namespace Tests
{
	public static class Program
	{
		private const string SelfProfileSdkDirectory = "dottrace-selfprofile";

		public static string InputDirPath { get; }
		public static string OutputDirPath { get; }
		private static string SdkPath { get; }
		private static string OutputPath { get; }

		static Program()
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

		public static int Main(string[] arguments)
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

			return 0;
#else
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Tests.exe is not built with Profiling support");
			Console.ResetColor();
			return 2;
#endif
		}
#if FEATURE_PROFILING
		private static IEnumerable<IProfileFactory> CreateProfilingFactory(ProfilingCluster cluster)
		{
			yield return new PerformanceProfileFactory(SdkPath, OutputPath, cluster, Assembly.GetEntryAssembly(), new ColoredConsoleWriter());
			yield return new TimelineProfileFactory(SdkPath, OutputPath, cluster, Assembly.GetEntryAssembly(), new ColoredConsoleWriter());
			yield return new MemoryProfileFactory(SdkPath, OutputPath, cluster, Assembly.GetEntryAssembly(), new ColoredConsoleWriter());
		}
#endif
	}
}

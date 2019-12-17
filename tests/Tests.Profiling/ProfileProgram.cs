using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tests.Profiling.Framework;
using Tests.Profiling.Framework.Memory;
using Tests.Profiling.Framework.Performance;
using Tests.Profiling.Framework.Timeline;

namespace Tests.Profiling
{
	public static class Program
	{

		private static string SdkPath { get; }
		private static string OutputPath { get; }

		static Program()
		{
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
			var sdkDir = FindSelfProfileSdkDirectory(currentDirectory);
			if (sdkDir == null)
				throw new Exception($"Can not find {SelfProfileSdkDirectory} starting from {currentDirectory.FullName}");

			SdkPath = sdkDir.FullName;
			OutputPath = Path.Combine(sdkDir.Parent.Parent.FullName, "profiling");
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
				cluster.Start();
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

		private const string SelfProfileSdkDirectory = "dottrace-selfprofile";
		private static DirectoryInfo FindSelfProfileSdkDirectory(DirectoryInfo directoryInfo)
		{
			do
			{
				var sdkDir = Path.Combine(directoryInfo.FullName, "build", "tools", SelfProfileSdkDirectory);
				if (Directory.Exists(sdkDir)) return new DirectoryInfo(sdkDir);
				directoryInfo = directoryInfo.Parent;
			} while (directoryInfo != null);
			return null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Elastic.BenchmarkDotNetExporter;
using LibGit2Sharp;
using Tests.Benchmarking.Framework;
using RunMode = BenchmarkDotNet.Jobs.RunMode;

namespace Tests.Benchmarking
{
	public static class Program
	{
		private static string Commit { get; }
		private static string CommitMessage { get; }
		private static string Branch { get; }
		private static string Repository { get; }

		static Program()
		{
			var dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
			while(dirInfo != dirInfo.Root && !Directory.Exists(Path.Combine(dirInfo.FullName, ".git")))
				dirInfo = dirInfo.Parent;
			if (!Directory.Exists(Path.Combine(dirInfo.FullName, ".git"))) Environment.Exit(2);

			Console.WriteLine(dirInfo.FullName);
			using (var repos = new Repository(dirInfo.FullName))
			{
				Commit = repos.Head.Tip.Sha;
				CommitMessage = repos.Head.Tip.Message;
				Branch = repos.Head.FriendlyName;
				var remoteName = repos.Head.RemoteName;
				Repository =
					repos.Network.Remotes.FirstOrDefault(r => r.Name == remoteName)?.Url
					?? repos.Network.Remotes.FirstOrDefault()?.Url;
			}
		}

		public static int Main(string[] arguments)
		{
			Console.WriteLine($"Tests.Benchmarking: [{Branch}]@({Commit}) on {Repository} : {CommitMessage} - ");
			return 0;
			var config = CreateDefaultConfig();
			if (arguments.Any() && arguments[0].Equals("--all", StringComparison.OrdinalIgnoreCase))
			{
				Console.WriteLine("Running all the benchmarks");
				return RunAllBenchmarks(config, arguments.Skip(1).ToArray());
			}

			Console.WriteLine("Running the interactive benchmark switcher.");
			var benchmarkSwitcher = new BenchmarkSwitcher(GetBenchmarkTypes());
			config = config.With(MarkdownExporter.GitHub);
			benchmarkSwitcher.Run(arguments, config);
			return 0;
		}

		private static IConfig CreateDefaultConfig()
		{
			var jobs = new[]
			{
				Job.ShortRun.With(Runtime.Core).With(Jit.RyuJit),
//				Job.ShortRun.With(Runtime.Clr).With(Jit.RyuJit),
//				Job.ShortRun.With(Runtime.Clr).With(Jit.LegacyJit),
			};
			var config = DefaultConfig.Instance
				.With(jobs)
				.With(MemoryDiagnoser.Default);
			return config;
		}

		private static int RunAllBenchmarks(IConfig config, string[] arguments)
		{
			var url = arguments.Length > 0 ? arguments[0] : null;
			var username = arguments.Length > 1 ? arguments[1] : null;
			var password = arguments.Length > 2 ? arguments[2] : null;

			Console.WriteLine("Running in Non-Interactive mode.");

			var exporter = CreateElasticsearchExporter(url, username, password);
			foreach (var benchmarkType in GetBenchmarkTypes())
			{
				if (exporter != null) config = config.With(exporter);
				BenchmarkRunner.Run(benchmarkType, config);
			}

			return 0;
		}

		private static ElasticsearchBenchmarkExporter CreateElasticsearchExporter(string url, string username, string password)
		{
			if (string.IsNullOrWhiteSpace(url)) return null;
			var options = new ElasticsearchBenchmarkExporterOptions(url)
			{
				Username = username,
				Password = password,
				GitCommitSha = Commit,
				GitBranch = Branch,
				GitCommitMessage = CommitMessage
			};
			return new ElasticsearchBenchmarkExporter(options);
		}


		private static Type[] GetBenchmarkTypes()
		{
			IEnumerable<Type> types;

			try
			{
				types = typeof(Program).GetTypeInfo().Assembly.GetTypes();
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

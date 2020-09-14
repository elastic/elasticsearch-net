// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
using Elastic.CommonSchema.BenchmarkDotNetExporter;

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
//			using (var repos = new Repository(dirInfo.FullName))
//			{
//				Commit = repos.Head.Tip.Sha;
//				CommitMessage = repos.Head.Tip.Message?.Trim(' ', '\t', '\r', '\n');
//				Branch = repos.Head.FriendlyName;
//				var remoteName = repos.Head.RemoteName;
//				Repository =
//					repos.Network.Remotes.FirstOrDefault(r => r.Name == remoteName)?.Url
//					?? repos.Network.Remotes.FirstOrDefault()?.Url;
//			}
		}

		public static int Main(string[] arguments)
		{
			//Console.WriteLine($"Tests.Benchmarking: [{Branch}]@({Commit}) on {Repository} : {CommitMessage} - ");
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
			var jobs = new List<Job>
			{
				Job.Default.With(CoreRuntime.Core30).With(Jit.RyuJit),
			};

			var config = DefaultConfig.Instance
				.With(jobs.ToArray())
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
				GitCommitMessage = CommitMessage,
				GitRepositoryIdentifier = Repository
			};
			return new ElasticsearchBenchmarkExporter(options);
		}


		private static Type[] GetBenchmarkTypes()
		{
			IEnumerable<Type> types;

			try
			{
				types = typeof(Program).Assembly.GetTypes();
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

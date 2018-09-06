using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Elastic.Xunit;
using Tests.Configuration;

namespace Tests.Core.Xunit
{
	/// <summary> Feeding TestClient.Configuration options to the runner</summary>
	public class NestXunitRunOptions : ElasticXunitRunOptions
	{
		public NestXunitRunOptions()
		{
			this.RunIntegrationTests = TestConfiguration.Instance.RunIntegrationTests;
			this.RunUnitTests = TestConfiguration.Instance.RunUnitTests;
			this.ClusterFilter = TestConfiguration.Instance.ClusterFilter;
			this.TestFilter = TestConfiguration.Instance.TestFilter;
			this.Version = TestConfiguration.Instance.ElasticsearchVersion;
			this.IntegrationTestsMayUseAlreadyRunningNode = TestConfiguration.Instance.TestAgainstAlreadyRunningElasticsearch;

			Generators.Initialize();
		}

		public override void OnBeforeTestsRun() => DumpConfiguration();

		private static void DumpConfiguration()
		{
			var config = TestConfiguration.Instance;

			Console.WriteLine(new string('-', 20));
			Console.WriteLine("Starting tests using config:");
			Console.WriteLine($" - {nameof(config.TestAgainstAlreadyRunningElasticsearch)}: {config.TestAgainstAlreadyRunningElasticsearch}");
			Console.WriteLine($" - {nameof(config.ElasticsearchVersion)}: {config.ElasticsearchVersion}");
			Console.WriteLine($" - {nameof(config.ForceReseed)}: {config.ForceReseed}");
			Console.WriteLine($" - {nameof(config.Mode)}: {config.Mode}");
			Console.WriteLine($" - {nameof(config.Seed)}: {config.Seed}");
			if (config.Mode == TestMode.Integration)
			{
				Console.WriteLine($" - {nameof(config.ClusterFilter)}: {config.ClusterFilter}");
				Console.WriteLine($" - {nameof(config.TestFilter)}: {config.TestFilter}");

			}
			Console.WriteLine($" - {nameof(config.RunIntegrationTests)}: {config.RunIntegrationTests}");
			Console.WriteLine($" - {nameof(config.RunUnitTests)}: {config.RunUnitTests}");
			Console.WriteLine($" - Random:");
			Console.WriteLine($" \t- {nameof(config.Random.SourceSerializer)}: {config.Random.SourceSerializer}");
			Console.WriteLine($" \t- {nameof(config.Random.TypedKeys)}: {config.Random.TypedKeys}");
			Console.WriteLine(new string('-', 20));

		}

		public override void OnTestsFinished(Dictionary<string, Stopwatch> clusterTotals, ConcurrentBag<Tuple<string, string>> failedCollections)
		{
			Console.Out.Flush();
			DumpClusterTotals(clusterTotals);
			DumpSeenDeprecations();
			DumpFailedCollections(failedCollections);
		}

		private static void DumpClusterTotals(Dictionary<string, Stopwatch> clusterTotals)
		{
			Console.WriteLine("--------");
			Console.WriteLine("Individual cluster running times:");
			foreach (var kv in clusterTotals) Console.WriteLine($"- {kv.Key}: {kv.Value.Elapsed}");
			Console.WriteLine("--------");
		}
		private static void DumpSeenDeprecations()
		{
			if (XunitRunState.SeenDeprecations.Count == 0) return;

			Console.WriteLine("-------- SEEN DEPRECATIONS");
			foreach (var d in XunitRunState.SeenDeprecations.Distinct())
				Console.WriteLine(d);
			Console.WriteLine("--------");
		}

		private static void DumpFailedCollections(ConcurrentBag<Tuple<string, string>> failedCollections)
		{
			if (failedCollections.Count <= 0) return;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Failed collections:");
			foreach (var t in failedCollections.OrderBy(p => p.Item1).ThenBy(t => t.Item2))

			{
				var cluster = t.Item1;
				Console.WriteLine($" - {cluster}: {t.Item2}");
			}
			DumpReproduceFilters(failedCollections);
			Console.ResetColor();
		}

		private static void DumpReproduceFilters(ConcurrentBag<Tuple<string, string>> failedCollections)
		{
			var config = TestConfiguration.Instance;
			var runningIntegrations = config.RunIntegrationTests;
			Console.ForegroundColor = ConsoleColor.Yellow;
			///
			Console.WriteLine("---Reproduce: -----");
			var reproduceLine = ReproduceCommandLine(failedCollections, config, runningIntegrations);
			Console.WriteLine(reproduceLine);
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")))
				Console.WriteLine($"##teamcity[buildProblem description='{reproduceLine}']");
			Console.WriteLine("--------");
		}

		private static string ReproduceCommandLine(ConcurrentBag<Tuple<string, string>> failedCollections, ITestConfiguration config, bool runningIntegrations)
		{
			var sb = new StringBuilder("build ")
				.Append($"seed:{config.Seed} ");

			AppendExplictConfig(nameof(RandomConfiguration.SourceSerializer), sb);
			AppendExplictConfig(nameof(RandomConfiguration.TypedKeys), sb);

			if (runningIntegrations)
				sb.Append("integrate ")
					.Append(TestConfiguration.Instance.ElasticsearchVersion);

			else sb.Append("test");

			if (runningIntegrations && failedCollections.Count > 0)
			{
				var clusters = string.Join(",", failedCollections
					.Select(c => c.Item1.ToLowerInvariant()).Distinct());
				sb.Append(" \"");
				sb.Append(clusters);
				sb.Append("\"");
			}

			if ((!runningIntegrations || (failedCollections.Count < 30)) && failedCollections.Count > 0)
			{
				sb.Append(" \"");
				var tests = string.Join(",", failedCollections
					.OrderBy(t => t.Item2)
					.Select(c => c.Item2.ToLowerInvariant().Split('.').Last()
						.Replace("apitests", "")
						.Replace("usagetests", "")
						.Replace("tests", "")
					));
				sb.Append(tests);
				sb.Append("\"");
			}

			var reproduceLine = sb.ToString();
			return reproduceLine;
		}

		/// <summary>
		/// Append random overwrite to reproduce line only if one was provided explicitly
		/// </summary>
		private static void AppendExplictConfig(string key, StringBuilder sb)
		{
			if (!TryGetExplicitRandomConfig(key, out var b)) return;
			sb.Append($"random:{key}{(b ? "" : ":false")} ");
		}

		private static bool TryGetExplicitRandomConfig(string key, out bool value)
		{
			value = false;
			var v = Environment.GetEnvironmentVariable($"NEST_RANDOM_{key.ToUpper()}");
			return !string.IsNullOrWhiteSpace(v) && bool.TryParse(v, out value);
		}

	}
}

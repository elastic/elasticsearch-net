using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Bogus;
using Elastic.Xunit;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch;
using static System.Console;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(NestXunitRunOptions))]

namespace Tests.Framework.ManagedElasticsearch
{
	/// <summary> Feeding TestClient.Configuration options to the runner</summary>
	public class NestXunitRunOptions : ElasticXunitRunOptions
	{
		public NestXunitRunOptions()
		{
			this.RunIntegrationTests = TestClient.Configuration.RunIntegrationTests;
			this.RunUnitTests = TestClient.Configuration.RunUnitTests;
			this.ClusterFilter = TestClient.Configuration.ClusterFilter;
			this.TestFilter = TestClient.Configuration.TestFilter;

			DumpConfiguration();
		}

		private static void DumpConfiguration()
		{
			var config = TestClient.Configuration;

			Randomizer.Seed = new Random(config.Seed);

			WriteLine(new string('-', 20));
			WriteLine("Starting tests using config:");
			WriteLine($" - {nameof(config.TestAgainstAlreadyRunningElasticsearch)}: {config.TestAgainstAlreadyRunningElasticsearch}");
			WriteLine($" - {nameof(config.ElasticsearchVersion)}: {config.ElasticsearchVersion}");
			WriteLine($" - {nameof(config.ForceReseed)}: {config.ForceReseed}");
			WriteLine($" - {nameof(config.Mode)}: {config.Mode}");
			WriteLine($" - {nameof(config.Seed)}: {config.Seed}");
			if (config.Mode == TestMode.Integration)
			{
				WriteLine($" - {nameof(config.ClusterFilter)}: {config.ClusterFilter}");
				WriteLine($" - {nameof(config.TestFilter)}: {config.TestFilter}");

			}
			WriteLine($" - {nameof(config.RunIntegrationTests)}: {config.RunIntegrationTests}");
			WriteLine($" - {nameof(config.RunUnitTests)}: {config.RunUnitTests}");
			WriteLine($" - Random:");
			WriteLine($" \t- {nameof(config.Random.SourceSerializer)}: {config.Random.SourceSerializer}");
			WriteLine($" \t- {nameof(config.Random.TypedKeys)}: {config.Random.TypedKeys}");
			WriteLine($" \t- {nameof(config.Random.OldConnection)}: {config.Random.OldConnection}");
			WriteLine(new string('-', 20));

		}

		public override void OnTestsFinished(Dictionary<string, Stopwatch> clusterTotals, ConcurrentBag<Tuple<string, string>> failedCollections)
		{
			Out.Flush();
			DumpClusterTotals(clusterTotals);
			DumpSeenDeprecations();
			DumpFailedCollections(failedCollections);
		}

		private static void DumpClusterTotals(Dictionary<string, Stopwatch> clusterTotals)
		{
			WriteLine("--------");
			WriteLine("Individual cluster running times:");
			foreach (var kv in clusterTotals) WriteLine($"- {kv.Key}: {kv.Value.Elapsed}");
			WriteLine("--------");
		}
		private static void DumpSeenDeprecations()
		{
			if (TestClient.SeenDeprecations.Count == 0) return;

			WriteLine("-------- SEEN DEPRECATIONS");
			foreach (var d in TestClient.SeenDeprecations.Distinct())
				WriteLine(d);
			WriteLine("--------");
		}

		private static void DumpFailedCollections(ConcurrentBag<Tuple<string, string>> failedCollections)
		{
			if (failedCollections.Count <= 0) return;

			ForegroundColor = ConsoleColor.Red;
			WriteLine("Failed collections:");
			foreach (var t in failedCollections.OrderBy(p => p.Item1).ThenBy(t => t.Item2))

			{
				var cluster = t.Item1;
				WriteLine($" - {cluster}: {t.Item2}");
			}
			DumpReproduceFilters(failedCollections);
			ResetColor();
		}

		private static void DumpReproduceFilters(ConcurrentBag<Tuple<string, string>> failedCollections)
		{
			var config = TestClient.Configuration;
			var runningIntegrations = config.RunIntegrationTests;
			ForegroundColor = ConsoleColor.Yellow;
			WriteLine("---Reproduce: -----");
			var sb = new StringBuilder("build ")
				.Append($"seed:{config.Seed} ");

			AppendExplictConfig(nameof(RandomConfiguration.SourceSerializer), sb);
			AppendExplictConfig(nameof(RandomConfiguration.TypedKeys), sb);
#if FEATURE_HTTPWEBREQUEST
			AppendExplictConfig(nameof(RandomConfiguration.OldConnection), sb);
#endif

			if (runningIntegrations)
				sb.Append("integrate ")
					.Append(TestClient.Configuration.ElasticsearchVersion);

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
			WriteLine(sb.ToString());
			WriteLine("--------");
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

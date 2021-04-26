/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Elastic.Elasticsearch.Xunit;
using Tests.Configuration;

namespace Tests.Core.Xunit
{
	/// <summary> Feeding TestClient.Configuration options to the runner</summary>
	public class NestXunitRunOptions : ElasticXunitRunOptions
	{
		public NestXunitRunOptions()
		{
			RunIntegrationTests = TestConfiguration.Instance.RunIntegrationTests;
			RunUnitTests = TestConfiguration.Instance.RunUnitTests;
			ClusterFilter = TestConfiguration.Instance.ClusterFilter;
			TestFilter = TestConfiguration.Instance.TestFilter;
			Version = TestConfiguration.Instance.ElasticsearchVersion;
			IntegrationTestsMayUseAlreadyRunningNode = TestConfiguration.Instance.TestAgainstAlreadyRunningElasticsearch;

			Generators.Initialize();
		}

		public override void OnBeforeTestsRun() => TestConfiguration.Instance.DumpConfiguration();

		public override void OnTestsFinished(Dictionary<string, Stopwatch> clusterTotals, ConcurrentBag<Tuple<string, string>> failedCollections)
		{
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
			if (failedCollections.Count <= 0)
			{
				var config = TestConfiguration.Instance;
				var runningIntegrations = config.RunIntegrationTests;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("---Reproduce: -----");
				var reproduceLine = ReproduceCommandLine(failedCollections, config, runningIntegrations);
				Console.WriteLine(reproduceLine);
				Console.WriteLine("---------------");
				Console.ResetColor();
				return;
			};

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
			Console.WriteLine("---Reproduce: -----");
			var reproduceLine = ReproduceCommandLine(failedCollections, config, runningIntegrations);
			Console.WriteLine(reproduceLine);
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION")))
				Console.WriteLine($"##teamcity[buildProblem description='{reproduceLine}']");
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD")))
			{
				var count = failedCollections.Count;
				Console.WriteLine($"##vso[task.logissue type=error;]{count} test failures");
				Console.WriteLine($"##vso[task.logissue type=error;]{reproduceLine}");
			}
			Console.WriteLine("--------");
		}

		private static string ReproduceCommandLine(ConcurrentBag<Tuple<string, string>> failedCollections, TestConfigurationBase config,
			bool runningIntegrations
		)
		{
			var sb = new StringBuilder(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".\\build.bat " : "./build.sh ")
				.Append("seed:").Append(config.Seed).Append(" ");

			AppendConfig(nameof(RandomConfiguration.SourceSerializer), config.Random.SourceSerializer, sb);
			AppendConfig(nameof(RandomConfiguration.TypedKeys), config.Random.TypedKeys, sb);
			AppendConfig(nameof(RandomConfiguration.HttpCompression), config.Random.HttpCompression, sb);
			AppendConfig(nameof(RandomConfiguration.ApiVersioning), config.Random.ApiVersioning, sb);

			if (runningIntegrations)
				sb.Append("integrate ")
					.Append(TestConfiguration.Instance.ElasticsearchVersion);
			else
				sb.Append("test");

			if (runningIntegrations && failedCollections.Count > 0)
			{
				var clusters = string.Join(",", failedCollections
					.Select(c => c.Item1.ToLowerInvariant())
					.Distinct());
				sb.Append(" \"");
				sb.Append(clusters);
				sb.Append("\"");
			}

			if ((!runningIntegrations || failedCollections.Count < 30) && failedCollections.Count > 0)
			{
				sb.Append(" \"");
				var tests = string.Join(",", failedCollections
					.OrderBy(t => t.Item2)
					.Select(c => c.Item2.ToLowerInvariant()
						.Split('.')
						.Last()
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
		/// Append random values used
		/// </summary>
		private static void AppendConfig(string key, bool value, StringBuilder sb) =>
			sb.Append($"random:{key.ToLowerInvariant()}{(value ? "" : ":false")} ");
	}
}

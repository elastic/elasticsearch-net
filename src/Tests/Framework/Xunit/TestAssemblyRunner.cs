using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;
using System.Reflection;
using Tests.Framework;
using System.Collections.Concurrent;
using Tests.Framework.Integration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Xunit
{
	class TestAssemblyRunner : XunitTestAssemblyRunner
	{
		readonly Dictionary<Type, object> assemblyFixtureMappings = new Dictionary<Type, object>();

		public TestAssemblyRunner(ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
		{
		}

		protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus bus, ITestCollection col,
			IEnumerable<IXunitTestCase> testCases, CancellationTokenSource cts) =>
			new TestCollectionRunner(
				assemblyFixtureMappings, col, testCases, DiagnosticMessageSink, bus, TestCaseOrderer,
				new ExceptionAggregator(Aggregator), cts
			).RunAsync();

		private class GroupedByCluster
		{
			public ClusterBase Cluster { get; set; }
			public ITestCollection Collection { get; set; }
			public List<IXunitTestCase> TestCases { get; set; }
		}

		protected override async Task<RunSummary> RunTestCollectionsAsync(IMessageBus messageBus,
			CancellationTokenSource cancellationTokenSource)
		{
			//bit side effecty, sets up assemblyFixtureMapping before possibly letting xunit do its regular concurrency thing
			var grouped = (from c in OrderTestCollections()
				let cluster = ClusterFixture(c.Item1)
				let testcase = new GroupedByCluster {Collection = c.Item1, TestCases = c.Item2, Cluster = cluster}
				group testcase by testcase.Cluster
				into g
				orderby g.Count() descending
				select g).ToList();

			//threading guess
			var defaultMaxConcurrency = Environment.ProcessorCount * 4;

			if (TestClient.Configuration.RunIntegrationTests)
				return await IntegrationPipeline(defaultMaxConcurrency, grouped, messageBus, cancellationTokenSource);
			return await UnitTestPipeline(defaultMaxConcurrency, grouped, messageBus, cancellationTokenSource);
		}

		private static readonly TimeSpan SlowTestThreshold = TimeSpan.FromSeconds(2.0);

		private async Task<RunSummary> UnitTestPipeline(int defaultMaxConcurrency,
			List<IGrouping<ClusterBase, GroupedByCluster>> grouped, IMessageBus messageBus,
			CancellationTokenSource cancellationTokenSource)
		{
			foreach (var g in grouped) g.Key?.Start();

			var summaries = new ConcurrentBag<RunSummary>();
			var slowTestCollections = new ConcurrentDictionary<string, TimeSpan>();

			var testFilter = TestClient.Configuration.TestFilter;
			var sw = Stopwatch.StartNew();
			await grouped.SelectMany(g => g)
				.ForEachAsync(defaultMaxConcurrency, async g =>
				{
					var test = g.Collection.DisplayName.Replace("Test collection for", "");
					if (!string.IsNullOrWhiteSpace(testFilter) && test.IndexOf(testFilter, StringComparison.OrdinalIgnoreCase) < 0)
						return;

					if (!string.IsNullOrWhiteSpace(testFilter)) Console.WriteLine(" -> " + test);

					try
					{
						var s = Stopwatch.StartNew();
						var summary = await RunTestCollectionAsync(messageBus, g.Collection, g.TestCases, cancellationTokenSource);
						s.Stop();
						if (s.Elapsed >= SlowTestThreshold)
							slowTestCollections.TryAdd(test, s.Elapsed);
						summaries.Add(summary);
					}
					catch (TaskCanceledException)
					{
					}
				});
			sw.Stop();
			foreach (var g in grouped) g.Key?.Dispose();

			Console.WriteLine("--------");
			Console.WriteLine($"Unit test time {sw.Elapsed}");
			Console.WriteLine("--------");
			if (slowTestCollections.Count > 0)
			{
				Console.WriteLine($"Test collections slower then {SlowTestThreshold}");
				foreach (var t in slowTestCollections.OrderByDescending(kv => kv.Value))
					Console.WriteLine($"  ({t.Value}) - {t.Key}");
			}

			return new RunSummary()
			{
				Total = summaries.Sum(s => s.Total),
				Failed = summaries.Sum(s => s.Failed),
				Skipped = summaries.Sum(s => s.Skipped)
			};
		}

		private IEnumerable<string> ParseExcludedClusters(string clusterFilter)
		{
			if (string.IsNullOrWhiteSpace(clusterFilter)) return Enumerable.Empty<string>();
			var clusters =
#if DOTNETCORE
				typeof(ClusterBase).Assembly()
#else
				typeof(ClusterBase).Assembly
#endif
#endif
				.GetTypes()
				.Where(t => typeof(ClusterBase).IsAssignableFrom(t) && t != typeof(ClusterBase))
				.Select(c => c.Name.Replace("Cluster", "").ToLowerInvariant());
			var filters = clusterFilter.Split(',').Select(c => c.Trim().ToLowerInvariant());
			var include = filters.Where(f => !f.StartsWith("-")).Select(f => f.ToLowerInvariant());
			if (include.Any()) return clusters.Where(c => !include.Contains(c));
			var exclude = filters.Where(f => f.StartsWith("-")).Select(f => f.TrimStart('-').ToLowerInvariant());
			if (exclude.Any()) return exclude;
			return new List<string>();
		}

		private async Task<RunSummary> IntegrationPipeline(int defaultMaxConcurrency,
			List<IGrouping<ClusterBase, GroupedByCluster>> grouped, IMessageBus messageBus,
			CancellationTokenSource cancellationTokenSource)
		{
			var summaries = new ConcurrentBag<RunSummary>();
			var clusterTotals = new Dictionary<string, Stopwatch>();
			var excludedClusters = ParseExcludedClusters(TestClient.Configuration.ClusterFilter);
			var testFilter = TestClient.Configuration.TestFilter;
			foreach (var group in grouped)
			{
				var type = @group.Key?.GetType();
				var clusterName = type?.Name.Replace("Cluster", "") ?? "UNKNOWN";

				if (excludedClusters.Contains(clusterName, StringComparer.OrdinalIgnoreCase))
					continue;

				var dop = @group.Key != null && @group.Key.MaxConcurrency > 0
					? @group.Key.MaxConcurrency
					: defaultMaxConcurrency;

				clusterTotals.Add(clusterName, Stopwatch.StartNew());

				//We group over each cluster group and execute test classes pertaining to that cluster
				//in parallel
				using (@group.Key ?? System.Reactive.Disposables.Disposable.Empty)
				{
					@group.Key?.Start();
					await @group.ForEachAsync(dop, async g =>
					{
						var test = g.Collection.DisplayName.Replace("Test collection for", "");
						if (!string.IsNullOrWhiteSpace(testFilter) && test.IndexOf(testFilter, StringComparison.OrdinalIgnoreCase) < 0)
							return;

						if (!string.IsNullOrWhiteSpace(testFilter)) Console.WriteLine(" -> " + test);

						try
						{
							var summary = await RunTestCollectionAsync(messageBus, g.Collection, g.TestCases, cancellationTokenSource);
							summaries.Add(summary);
						}
						catch (TaskCanceledException)
						{
						}
					});
				}
				clusterTotals[clusterName].Stop();
			}

			Console.WriteLine("--------");
			Console.WriteLine("Individual cluster running times");
			foreach (var kv in clusterTotals)
				Console.WriteLine($"- {kv.Key}: {kv.Value.Elapsed.ToString()}");
			Console.WriteLine("--------");


			return new RunSummary()
			{
				Total = summaries.Sum(s => s.Total),
				Failed = summaries.Sum(s => s.Failed),
				Skipped = summaries.Sum(s => s.Skipped)
			};
		}

		protected ClusterBase ClusterFixture(ITestCollection testCollection)
		{
			var clusterType = GetClusterForCollection(testCollection);

			ClusterBase cluster = null;
			if (clusterType == null) return null;
			if (assemblyFixtureMappings.ContainsKey(clusterType))
				return assemblyFixtureMappings[clusterType] as ClusterBase;
			Aggregator.Run(() =>
			{
				var o = Activator.CreateInstance(clusterType);
				assemblyFixtureMappings.Add(clusterType, o);
				cluster = o as ClusterBase;
			});
			return cluster;
		}

		public static Type GetClusterForCollection(ITestCollection testCollection)
		{
			var collectionTypeName = testCollection.DisplayName.Split(' ').Last();
			var collectionType = Type.GetType(collectionTypeName);
			var clusterType = collectionType.GetTypeInfo()
				.ImplementedInterfaces
				.Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IClusterFixture<>))
				.Select(i => i.GetTypeInfo().GenericTypeArguments.Single())
				.FirstOrDefault();
			return clusterType;
		}
	}
}

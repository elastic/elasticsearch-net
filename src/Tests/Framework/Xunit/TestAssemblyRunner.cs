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
using System.Reactive.Disposables;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Xunit
{
	class TestAssemblyRunner : XunitTestAssemblyRunner
	{
		private readonly Dictionary<Type, object> _assemblyFixtureMappings = new Dictionary<Type, object>();
		private readonly List<IGrouping<ClusterBase, GroupedByCluster>> _grouped;
		private static readonly object _lock = new object();

		public ConcurrentBag<RunSummary> Summaries { get; } = new ConcurrentBag<RunSummary>();
		public ConcurrentBag<Tuple<string, string>> FailedCollections { get; } = new ConcurrentBag<Tuple<string, string>>();
		public Dictionary<string, Stopwatch> ClusterTotals { get; } = new Dictionary<string, Stopwatch>();

		private class GroupedByCluster
		{
			public ClusterBase Cluster { get; set; }
			public ITestCollection Collection { get; set; }
			public List<IXunitTestCase> TestCases { get; set; }
		}

		public TestAssemblyRunner(ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
		{
			//bit side effecty, sets up _assemblyFixtureMappings before possibly letting xunit do its regular concurrency thing
			this._grouped = (from c in OrderTestCollections()
				let cluster = ClusterFixture(c.Item1)
				let testcase = new GroupedByCluster {Collection = c.Item1, TestCases = c.Item2, Cluster = cluster}
				group testcase by testcase.Cluster
				into g
				orderby g.Count() descending
				select g)
				.ToList();
		}

		protected override Task<RunSummary> RunTestCollectionAsync(
			IMessageBus b, ITestCollection c, IEnumerable<IXunitTestCase> t, CancellationTokenSource s
		)
		{
			var aggregator = new ExceptionAggregator(Aggregator);
			return new TestCollectionRunner(
				_assemblyFixtureMappings, c, t, DiagnosticMessageSink, b, TestCaseOrderer, aggregator, s
			).RunAsync();
		}

		protected override async Task<RunSummary> RunTestCollectionsAsync(IMessageBus messageBus,
			CancellationTokenSource cancellationTokenSource)
		{
			//threading guess
			var defaultMaxConcurrency = Environment.ProcessorCount * 4;

			if (TestClient.Configuration.RunIntegrationTests)
				return await IntegrationPipeline(defaultMaxConcurrency, messageBus, cancellationTokenSource);
			return await UnitTestPipeline(defaultMaxConcurrency, messageBus, cancellationTokenSource);
		}


		private async Task<RunSummary> UnitTestPipeline(int defaultMaxConcurrency, IMessageBus messageBus, CancellationTokenSource ctx)
		{
			//make sure all clusters go in started state (won't actually start clusters in unit test mode)
			foreach (var g in this._grouped) g.Key?.Start();

			var testFilters = CreateTestFilters(TestClient.Configuration.TestFilter);
			await this._grouped.SelectMany(g => g)
				.ForEachAsync(defaultMaxConcurrency, async g => { await RunTestCollections(messageBus, ctx, g, testFilters); });
			foreach (var g in this._grouped) g.Key?.Dispose();

			return new RunSummary()
			{
				Total = this.Summaries.Sum(s => s.Total),
				Failed = this.Summaries.Sum(s => s.Failed),
				Skipped = this.Summaries.Sum(s => s.Skipped)
			};
		}

		private async Task<RunSummary> IntegrationPipeline(int defaultMaxConcurrency, IMessageBus messageBus, CancellationTokenSource ctx)
		{
			var excludedClusters = ParseExcludedClusters(TestClient.Configuration.ClusterFilter);
			var testFilters = CreateTestFilters(TestClient.Configuration.TestFilter);
			foreach (var group in this._grouped)
			{
				var type = @group.Key?.GetType();
				var clusterName = type?.Name.Replace("Cluster", "") ?? "UNKNOWN";

				if (excludedClusters.Contains(clusterName, StringComparer.OrdinalIgnoreCase))
					continue;

				var dop = @group.Key != null && @group.Key.MaxConcurrency > 0 ? @group.Key.MaxConcurrency : defaultMaxConcurrency;

				this.ClusterTotals.Add(clusterName, Stopwatch.StartNew());
				//We group over each cluster group and execute test classes pertaining to that cluster
				//in parallel
				using (@group.Key ?? Disposable.Empty)
				{
					@group.Key?.Start();
					await @group.ForEachAsync(dop, async g => { await RunTestCollections(messageBus, ctx, g, testFilters); });
				}
				this.ClusterTotals[clusterName].Stop();
			}

			return new RunSummary()
			{
				Total = this.Summaries.Sum(s => s.Total),
				Failed = this.Summaries.Sum(s => s.Failed),
				Skipped = this.Summaries.Sum(s => s.Skipped)
			};
		}

		private async Task RunTestCollections(IMessageBus messageBus, CancellationTokenSource ctx, GroupedByCluster g, string[] testFilters)
		{
			var test = g.Collection.DisplayName.Replace("Test collection for", "").Trim();
			if (!MatchesATestFilter(test, testFilters)) return;
			if (testFilters.Length > 0) Console.WriteLine(" -> " + test);

			try
			{
				var summary = await RunTestCollectionAsync(messageBus, g.Collection, g.TestCases, ctx);
				if (summary.Failed > 0)
					this.FailedCollections.Add(Tuple.Create(g.Cluster.GetType().Name.Replace("Cluster", ""), test));
				this.Summaries.Add(summary);
			}
			catch (TaskCanceledException)
			{
			}
		}

		private static string[] CreateTestFilters(string testFilters) =>
			testFilters?.Split(',').Select(s => s.Trim()).Where(s=>!string.IsNullOrWhiteSpace(s)).ToArray()
			?? new string[0] { };

		private static bool MatchesATestFilter(string test, IReadOnlyCollection<string> testFilters)
		{
			if (testFilters.Count == 0 || string.IsNullOrWhiteSpace(test)) return true;
			return testFilters
				.Any(filter => test.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
		}


		private ClusterBase ClusterFixture(ITestCollection testCollection)
		{
			var clusterType = GetClusterForCollection(testCollection);

			ClusterBase cluster = null;
			if (clusterType == null) return null;
			if (_assemblyFixtureMappings.ContainsKey(clusterType))
				return _assemblyFixtureMappings[clusterType] as ClusterBase;
			Aggregator.Run(() =>
			{
				var o = Activator.CreateInstance(clusterType);
				_assemblyFixtureMappings.Add(clusterType, o);
				cluster = o as ClusterBase;
			});
			return cluster;
		}

		private static Type GetClusterForCollection(ITestCollection testCollection)
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

		private IEnumerable<string> ParseExcludedClusters(string clusterFilter)
		{
			if (string.IsNullOrWhiteSpace(clusterFilter)) return Enumerable.Empty<string>();
			var clusters = GetAllClustersFromAssembly();

			var filters = clusterFilter.Split(',').Select(c => c.Trim().ToLowerInvariant()).ToArray();

			var include = filters.Where(f => !f.StartsWith("-")).Select(f => f.ToLowerInvariant()).ToArray();
			if (include.Any()) return clusters.Where(c => !include.Contains(c));

			var exclude = filters.Where(f => f.StartsWith("-")).Select(f => f.TrimStart('-').ToLowerInvariant()).ToArray();
			if (exclude.Any()) return exclude;

			return new List<string>();
		}

		private static IEnumerable<string> GetAllClustersFromAssembly()
		{
			var clusters =
#if DOTNETCORE
				typeof(ClusterBase).Assembly()
#else
				typeof(ClusterBase).Assembly
#endif
					.GetTypes()
					.Where(t => typeof(ClusterBase).IsAssignableFrom(t) && t != typeof(ClusterBase))
					.Select(c => c.Name.Replace("Cluster", "").ToLowerInvariant());
			return clusters;
		}
	}
}

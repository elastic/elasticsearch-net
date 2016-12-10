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

			//If we are not running any integration tests we do not care about only keeping a single IClusterFixture
			//active at a time, so let xunit do what it does best.
			if (!TestClient.Configuration.RunIntegrationTests)
			{
				var result = await base.RunTestCollectionsAsync(messageBus, cancellationTokenSource);
				foreach (var g in grouped) g.Key?.Dispose();
				return result;
			}

			//threading guess
			var defaultMaxConcurrency = Environment.ProcessorCount * 4;

			var summaries = new ConcurrentBag<RunSummary>();
			var clusterTotals = new Dictionary<string, Stopwatch>();
			var clusterFilter = TestClient.Configuration.ClusterFilter;
			var testFilter = TestClient.Configuration.TestFilter;
			foreach (var group in grouped)
			{
				var type = group.Key?.GetType();
				var clusterName = type?.Name.Replace("Cluster", "") ?? "UNKNOWN";

				if (!string.IsNullOrWhiteSpace(clusterFilter) && clusterName.IndexOf(clusterFilter, StringComparison.OrdinalIgnoreCase) < 0)
					continue;

				var dop = group.Key != null && group.Key.MaxConcurrency > 0
					? group.Key.MaxConcurrency
					: defaultMaxConcurrency;

				clusterTotals.Add(clusterName, Stopwatch.StartNew());

				//We group over each cluster group and execute test classes pertaining to that cluster
				//in parallel
				using (group.Key ?? System.Reactive.Disposables.Disposable.Empty)
				{
					group.Key?.Start();
					await group.ForEachAsync(dop, async g =>
					{
						var test = g.Collection.DisplayName.Replace("Test collection for", "");
						if (!string.IsNullOrWhiteSpace(testFilter) && test.IndexOf(testFilter, StringComparison.OrdinalIgnoreCase) < 0)
							return;

						//display tests we execute when we filter so we get confirmation on the command line we run the tests we expect
						if (!string.IsNullOrWhiteSpace(testFilter))
							Console.WriteLine(" -> " + test);

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
			var clusterType = collectionType.GetTypeInfo().ImplementedInterfaces
				.Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IClusterFixture<>))
				.Select(i => i.GetTypeInfo().GenericTypeArguments.Single())
				.FirstOrDefault();
			return clusterType;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using Elasticsearch.Net;
using Nest;
using FluentAssertions;
using System.Threading;
using System.Reactive.Linq;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Reproduce
{
	public class ConnectionReuseCluster : ClientTestClusterBase { }

	public class ConnectionReuseAndBalancing : ClusterTestClassBase<ConnectionReuseCluster>
	{
		private static bool IsCurlHandler { get; } = typeof(HttpClientHandler).Assembly().GetType("System.Net.Http.CurlHandler") != null;

		public ConnectionReuseAndBalancing(ConnectionReuseCluster cluster) : base(cluster) { }

		public IEnumerable<Project> MockDataGenerator(int numDocuments)
		{
			foreach (var i in Enumerable.Range(0, numDocuments))
				yield return new Project {Name = $"project-{i}"};
		}

		[I] public async Task IndexAndSearchABunch()
		{
			const int requestsPerIteration = 1000;
			var client = this.Client;

			await IndexMockData(client, requestsPerIteration);

			var statsRequest = new NodesStatsRequest(NodesStatsMetric.Http);
			for (var i = 0; i < 20; i++)
			{
				Task[] tasks = Enumerable.Range(0, requestsPerIteration)
					.Select(async (r) => await client.SearchAsync<Project>())
					.ToArray();
				Task.WaitAll(tasks);

				var nodeStats = await client.NodesStatsAsync(statsRequest);
				AssertHttpStats(client, nodeStats, i, requestsPerIteration);
			}
		}

		private static void AssertHttpStats(IElasticClient c, INodesStatsResponse r, int i, int requestsPerIteration)
		{
			const int leeWay = 10;
			var connectionLimit = c.ConnectionSettings.ConnectionLimit;
			var maxCurrent = connectionLimit;
			var maxCurrentOpen = connectionLimit + 1; //cluster bootstrap opens it own connections

			foreach (var node in r.Nodes.Values) //in our cluster we only have 1 node
			{
				node.Http.TotalOpened.Should().BeGreaterThan(2, "We want to see some concurrency");
				var h = node.Http;
				node.Http.CurrentOpen.Should().BeLessOrEqualTo(maxCurrentOpen, $"CurrentOpen exceed our connection limit {maxCurrent}");

				string errorMessage;
				int iterationMax;

				if (!IsCurlHandler)
				{
					//on non curl connections we expect full connection reuse
					//we allow some leeway on the maxOpened because of connections setup and teared down
					//during the initial bootstrap procudure from the test framework getting the cluster up.
					iterationMax = maxCurrent + leeWay;
					errorMessage = $"Total openend exceeded {maxCurrent} + leighway factor {leeWay}";
				}
				else
				{
					var m = Math.Max(2, i + 1) + 1;
					iterationMax = ((maxCurrent * m) / 2) + leeWay;
					errorMessage =
						$"Expected some socket bleeding but iteration {i} exceeded iteration specific max {iterationMax} = (({maxCurrent} * {m}) / 2) + {leeWay}";
				}
				node.Http.TotalOpened.Should().BeLessOrEqualTo(iterationMax, errorMessage);
				if (i == -1) return;

				Console.WriteLine(
					$"Current Open: {h.CurrentOpen}, Total Opened: {h.TotalOpened}, Iteration Max = {iterationMax}, Iteration: {i}, Total Searches {(i + 1) * requestsPerIteration}");
			}
		}

		private async Task IndexMockData(IElasticClient c, int requestsPerIteration)
		{
			var tokenSource = new CancellationTokenSource();
			await c.DeleteIndexAsync(Index<Project>(), cancellationToken: tokenSource.Token);
			var observableBulk = c.BulkAll(this.MockDataGenerator(100000), f => f
					.MaxDegreeOfParallelism(10)
					.BackOffTime(TimeSpan.FromSeconds(10))
					.BackOffRetries(2)
					.Size(1000)
					.RefreshOnCompleted()
				, tokenSource.Token);
			await observableBulk.ForEachAsync(x => { }, tokenSource.Token);
			var statsRequest = new NodesStatsRequest(NodesStatsMetric.Http);
			var nodeStats = await c.NodesStatsAsync(statsRequest, tokenSource.Token);
			AssertHttpStats(c, nodeStats, -1, requestsPerIteration);
		}
	}
}

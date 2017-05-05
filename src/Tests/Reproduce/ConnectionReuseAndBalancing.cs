using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

using Elasticsearch.Net;
using Nest;
using FluentAssertions;
using System.Threading;
using System.Reactive.Linq;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Reproduce
{
	public class ConnectionReuseCluster : ClusterBase { }
	public class ConnectionReuseAndBalancing : IClusterFixture<ConnectionReuseCluster>
	{
		private readonly ConnectionReuseCluster _cluster;

		public ConnectionReuseAndBalancing(ConnectionReuseCluster cluster)
		{
			_cluster = cluster;
		}

		public IEnumerable<Project> MockDataGenerator(int numDocuments)
		{
			foreach (var i in Enumerable.Range(0, numDocuments))
				yield return new Project { Name = $"project-{i}" };
		}

		[I] public async Task IndexAndSearchABunch()
		{
			var tokenSource = new CancellationTokenSource();
			var client = _cluster.Client;

			await client.DeleteIndexAsync(Index<Project>());
			var observableBulk = client.BulkAll(this.MockDataGenerator(100000), f => f
				.MaxDegreeOfParallelism(10)
				.BackOffTime(TimeSpan.FromSeconds(10))
				.BackOffRetries(2)
				.Size(1000)
				.RefreshOnCompleted()
			, tokenSource.Token);
			await observableBulk.ForEachAsync(x => { }, tokenSource.Token);
			var statsRequest = new NodesStatsRequest(NodesStatsMetric.Http);
			var nodeStats = await client.NodesStatsAsync(statsRequest);
			this.AssertHttpStats(nodeStats);
			for (var i = 0; i < 10; i++)
			{
				var taskList = new Task[1000];
				for (var t=0;t<1000;t++)
				{
					taskList[t] = client.SearchAsync<Project>();
				}
				//Parallel.For(0, 1000, c => client.Search<Project>(s => s));
				Task.WaitAll(taskList);

				nodeStats = await client.NodesStatsAsync(statsRequest);
				this.AssertHttpStats(nodeStats);
			}
		}

		private void AssertHttpStats(INodesStatsResponse response)
		{
			foreach(var node in response.Nodes.Values)
			{
				node.Http.TotalOpened.Should().BeGreaterThan(2);
				node.Http.TotalOpened.Should().BeLessThan(100);
				node.Http.CurrentOpen.Should().BeLessThan(100);
			}

		}
	}
}

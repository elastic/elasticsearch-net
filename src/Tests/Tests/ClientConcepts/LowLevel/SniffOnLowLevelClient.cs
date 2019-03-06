using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Core.Client.Settings;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;

namespace Tests.ClientConcepts.LowLevel
{
	public class SniffOnLowLevelClient : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public SniffOnLowLevelClient(ReadOnlyCluster cluster) => _cluster = cluster;

		[I] public void CanSniffUsingJustTheLowLevelClient()
		{
			var uri = TestConnectionSettings.CreateUri(_cluster.Nodes.First().Port ?? 9200);
			var sniffingConnectionPool = new SniffingConnectionPool(new[] { uri });
			var elasticClient = new ElasticLowLevelClient(new ConnectionConfiguration(sniffingConnectionPool));

			Func<DynamicResponse> act = () => elasticClient.ClusterHealth<DynamicResponse>();
			act.Invoking(s => s.Invoke()).ShouldNotThrow();
		}

	}
}

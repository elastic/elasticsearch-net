// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.ClientConcepts.Connection;
using Tests.Core.ManagedElasticsearch.Clusters;


namespace Tests.ClientConcepts.ConnectionPooling.Pinging
{
	public class PingTests : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public PingTests(ReadOnlyCluster cluster) => _cluster = cluster;

#if DOTNETCORE
		[I]
		public void UsesRelativePathForPing()
		{
			var pool = new StaticConnectionPool(new[] { new Uri("http://localhost:9200/elasticsearch/") });
			var settings = new ConnectionSettings(pool,
				new HttpConnectionTests.TestableHttpConnection(response =>
				{
					response.RequestMessage.RequestUri.AbsolutePath.Should().StartWith("/elasticsearch/");
				}));

			var client = new ElasticClient(settings);
			var healthResponse = client.Ping();
		}
#else
		[I]
		public void UsesRelativePathForPing()
		{
			var pool = new StaticConnectionPool(new[] { new Uri("http://localhost:9200/elasticsearch/") });
			var connection = new HttpWebRequestConnectionTests.TestableHttpWebRequestConnection();
			var settings = new ConnectionSettings(pool, connection);

			var client = new ElasticClient(settings);
			var healthResponse = client.Ping();

			connection.LastRequest.Address.AbsolutePath.Should().StartWith("/elasticsearch/");
		}
#endif
	}
}


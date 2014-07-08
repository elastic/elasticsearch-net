using System;
using Elasticsearch.Net;
using Elasticsearch.Net.ConnectionPool;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Failover
{

	/// <summary>
	/// I use this class to answer questions on github issues/stackoverflow
	/// Tests that are written here are subject to removal at any time
	/// </summary>
	[TestFixture]
	public class FailoverSniffOnStartupTests
	{
		[Test]
		public void FailoverShouldOnlyPingDeadNodes()
		{
			var seeds = new[]
			{
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9200"),
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds, randomizeOnStartup: false);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool)
				.SniffOnStartup();
			var client = new ElasticClient(connectionSettings);
			var rootNode = client.RootNodeInfo();
			var metrics = rootNode.ConnectionStatus.Metrics;
			
			//When the connectionpool is used for the first time the sniff call should already
			//know only 9200 is on and live, no need to ping
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);

			for (var i = 0; i < 3; i++)
			{
				rootNode = client.RootNodeInfo();
				metrics = rootNode.ConnectionStatus.Metrics;
				metrics.Requests.Count.Should().Be(1);
				metrics.Requests[0].Node.Port.Should().Be(9200);
				metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);
			}

		}

		[Test]
		public async void FailoverShouldOnlyPingDeadNodes_Async()
		{
			var seeds = new[]
			{
				new Uri("http://localhost:9202"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9200"),
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds, randomizeOnStartup: false);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool)
				.SniffOnStartup();
			var client = new ElasticClient(connectionSettings);
			var rootNode = await client.RootNodeInfoAsync();
			var metrics = rootNode.ConnectionStatus.Metrics;
			
			//When the connectionpool is used for the first time the sniff call should already
			//know only 9200 is on and live, no need to ping
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);

			for (var i = 0; i < 3; i++)
			{
				rootNode = await client.RootNodeInfoAsync();
				metrics = rootNode.ConnectionStatus.Metrics;
				metrics.Requests.Count.Should().Be(1);
				metrics.Requests[0].Node.Port.Should().Be(9200);
				metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);
			}

		}
	}
}
using System;
using Elasticsearch.Net;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Failover
{

	/// <summary>
	/// I use this class to answer questions on github issues/stackoverflow
	/// Tests that are written here are subject to removal at any time
	/// </summary>
	[TestFixture]
	public class FailoverSniffOnFaultTests
	{
		[Test]
		public void SniffOnFaultShouldGetCleanClusterState()
		{
			var seeds = new[]
			{
				ElasticsearchConfiguration.CreateBaseUri(9202),
				ElasticsearchConfiguration.CreateBaseUri(9201),
				ElasticsearchConfiguration.CreateBaseUri(9200),
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds, randomizeOnStartup: false);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool)
				.SniffOnConnectionFault();

			var client = new ElasticClient(connectionSettings);
			var rootNode = client.RootNodeInfo();
			var metrics = rootNode.ConnectionStatus.Metrics;
			
			metrics.Requests.Count.Should().Be(5);
			metrics.Requests[0].Node.Port.Should().Be(9202);
			metrics.Requests[0].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[1].Node.Port.Should().Be(9201);
			metrics.Requests[1].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[2].Node.Port.Should().Be(9200);
			metrics.Requests[2].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[3].Node.Port.Should().Be(9200);
			metrics.Requests[3].RequestType.Should().Be(RequestType.Sniff);

			metrics.Requests[4].Node.Port.Should().Be(9200);
			metrics.Requests[4].RequestType.Should().Be(RequestType.ElasticsearchCall);


			for (var i = 0; i < 3; i++)
			{
				rootNode = client.RootNodeInfo();
				metrics = rootNode.ConnectionStatus.Metrics;
				metrics.Requests[0].Node.Port.Should().Be(9200);
				metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);
				metrics.Requests.Count.Should().Be(1);
			}

		}

		[Test]
		public async void SniffOnFaultShouldGetCleanClusterState_Async()
		{
			var seeds = new[]
			{
				ElasticsearchConfiguration.CreateBaseUri(9202),
				ElasticsearchConfiguration.CreateBaseUri(9201),
				ElasticsearchConfiguration.CreateBaseUri(9200),
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds, randomizeOnStartup: false);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool)
				.SniffOnConnectionFault();

			var client = new ElasticClient(connectionSettings);
			var rootNode = await client.RootNodeInfoAsync();
			var metrics = rootNode.ConnectionStatus.Metrics;
			
			metrics.Requests.Count.Should().Be(5);
			metrics.Requests[0].Node.Port.Should().Be(9202);
			metrics.Requests[0].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[1].Node.Port.Should().Be(9201);
			metrics.Requests[1].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[2].Node.Port.Should().Be(9200);
			metrics.Requests[2].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[3].Node.Port.Should().Be(9200);
			metrics.Requests[3].RequestType.Should().Be(RequestType.Sniff);

			metrics.Requests[4].Node.Port.Should().Be(9200);
			metrics.Requests[4].RequestType.Should().Be(RequestType.ElasticsearchCall);


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
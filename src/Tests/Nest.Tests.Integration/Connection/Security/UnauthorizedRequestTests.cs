using Elasticsearch.Net;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using Nest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Connection.Security
{
	[TestFixture]
	//[Ignore]
	public class UnauthorizedRequestTests
	{
		[Test]
		public void StaticConnectionPool_DoesNotThrowOrRetry_On_401()
		{
			var nodes = new Uri[] {
				new Uri("http://invalid:user@localhost:9200"),
				new Uri("http://invalid:user@localhost:9201")
			};
			var pool = new StaticConnectionPool(nodes);
			var settings = new ConnectionSettings(pool)
				.MaximumRetries(3);
			var client = new ElasticClient(settings);
			var r = client.RootNodeInfo();
			r.IsValid.Should().BeFalse();
			r.ConnectionStatus.HttpStatusCode.Should().Be(401);
			r.ConnectionStatus.Metrics.Requests.Count.Should().Be(1);
			r.ConnectionStatus.Metrics.Requests[0].RequestType.Should().Be(RequestType.Ping);
			r.ConnectionStatus.NumberOfRetries.Should().Be(0);
		}

		[Test]
		public void Unauthorized_With_Ping_Disabled()
		{
			var nodes = new Uri[] {
				new Uri("http://invalid:user@localhost:9200"),
				new Uri("http://invalid:user@localhost:9201")
			};
			var pool = new StaticConnectionPool(nodes);
			var settings = new ConnectionSettings(pool)
				.MaximumRetries(3)
				.DisablePing();
			var client = new ElasticClient(settings);
			var r = client.RootNodeInfo();
			r.IsValid.Should().BeFalse();
			r.ConnectionStatus.HttpStatusCode.Should().Be(401);
			r.ConnectionStatus.Metrics.Requests.Count.Should().Be(1);
			r.ConnectionStatus.Metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);
			r.ConnectionStatus.NumberOfRetries.Should().Be(0);
		}

		[Test]
		public void SniffingConnectionPool_DoesNotThrowOrRetry_On_401()
		{
			var nodes = new Uri[] {
				new Uri("http://invalid:user@localhost:9200"),
				new Uri("http://invalid:user@localhost:9201")
			};
			var pool = new SniffingConnectionPool(nodes);
			var settings = new ConnectionSettings(pool)
				.MaximumRetries(3);

			var client = new ElasticClient(settings);
			var r = client.RootNodeInfo();
			r.IsValid.Should().BeFalse();
			r.ConnectionStatus.HttpStatusCode.Should().Be(401);
			r.ConnectionStatus.Metrics.Requests.Count.Should().Be(1);
			r.ConnectionStatus.NumberOfRetries.Should().Be(0);
		}
	}
}
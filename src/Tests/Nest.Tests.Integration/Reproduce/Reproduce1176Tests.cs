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

namespace Nest.Tests.Integration.Reproduce
{
	public class Reproduce1176Tests
	{
		[Test]
		public void MaxRetryExceptionInnerExceptionIsNull()
		{
			var nodes = new Uri[]
			{
				new Uri("http://invalid_host:9300"),
				new Uri("http://invalid_host:9400"),
				new Uri("http://invalid_host:9500")
			};
			var connectionPool = new StaticConnectionPool(nodes);
			var settings = new ConnectionSettings(connectionPool)
				.DisablePing();
			var client = new ElasticClient(settings);

			var maxRetryException = Assert.Throws<MaxRetryException>(() => client.GetIndex(g => g.Index("foo")));
			maxRetryException.InnerException.Should().NotBeNull();
			
			var aggregateException = maxRetryException.InnerException as AggregateException;
			aggregateException.Should().NotBeNull();
			aggregateException.InnerExceptions.Count.Should().Be(nodes.Count());
		}

		[Test]
		public void MaxRetryExceptionInnerExceptionIsNullAsync()
		{
			var nodes = new Uri[]
			{
				new Uri("http://invalid_host:9300"),
				new Uri("http://invalid_host:9400"),
				new Uri("http://invalid_host:9500")
			};
			var connectionPool = new StaticConnectionPool(nodes);
			var settings = new ConnectionSettings(connectionPool)
				.DisablePing();
			var client = new ElasticClient(settings);

			var maxRetryException = Assert.Throws<MaxRetryException>(async () => await client.GetIndexAsync(g => g.Index("foo")));
			maxRetryException.InnerException.Should().NotBeNull();

			var aggregateException = maxRetryException.InnerException as AggregateException;
			aggregateException.Should().NotBeNull();
			aggregateException.InnerExceptions.Count.Should().Be(nodes.Count());
		}
	}
}

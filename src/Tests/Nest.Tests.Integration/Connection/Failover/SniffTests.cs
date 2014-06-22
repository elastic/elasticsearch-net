using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Shared.Extensions;
using Shared.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Nest.Tests.Integration.Core.Bulk
{
	[TestFixture]
	public class SniffTests : IntegrationTests
	{
		[Test]
		public void IndexExistShouldNotThrowOn404()
		{
			var host = Test.Default.Host;
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";
			var connectionPool = new SniffingConnectionPool(new[] { new Uri("http://{0}:9200".F(host)) });
			var settings = new ConnectionSettings(connectionPool, ElasticsearchConfiguration.DefaultIndex)
				.SniffOnStartup();
			var client = new ElasticClient(settings);

			
		}
	}
}

using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.ConnectionPool;
using NUnit.Framework;

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

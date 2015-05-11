using System.Text;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using FluentAssertions;
using Nest.Tests.Integration;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce953Tests 
	{
		[Test]
		public void Calling_Refresh_UsingHttpClientConnection_DoesNotThrow()
		{

			var settings = ElasticsearchConfiguration.Settings()
				.EnableHttpCompression();
			var connection = new HttpClientConnection(settings);
			var client = new ElasticClient(settings, connection: connection);
			
			Assert.DoesNotThrow(()=> client.Refresh());
			Assert.DoesNotThrow(()=> client.Get<ElasticsearchProject>(NestTestData.Data.First().Id));
			Assert.DoesNotThrow(()=> client.Ping());

		}
		
		
	}
}

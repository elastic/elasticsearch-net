using Elasticsearch.Net.ConnectionPool;
using Nest;
using Nest.Tests.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	[Ignore]
	public class Reproduce1193Tests
	{
		[Test]
		public void SniffingConnectionPoolPingThrowsException()
		{
			//ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
			var nodes = new Uri[] { new Uri("https://localhost:9200") };
			var pool = new SniffingConnectionPool(nodes);
			var settings = new ConnectionSettings(pool)
				.SetBasicAuthentication("netuser", "admin")
				.SniffOnStartup();

			Assert.DoesNotThrow(() => new ElasticClient(settings));
		}
	}
}

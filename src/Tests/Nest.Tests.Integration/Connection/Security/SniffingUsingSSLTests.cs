using System.Net;
using System.Net.Security;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Thrift;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using Nest;
using Nest.Tests.Integration;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Connection.Security
{
	[TestFixture]
	[Ignore("Relies on having a local cluster with shield configured")]
	public class SniffingUsingSSLTests
	{
		[Test]
		public void NodesDiscoveredDuringSniffShouldBeHttps()
		{
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
			{
				return true;
			};
			var uris = new[]
			{
				new Uri("https://localhost:9200")
			};
			var connectionPool = new SniffingConnectionPool(uris, randomizeOnStartup: false);
			var settings = new ConnectionSettings(connectionPool, ElasticsearchConfiguration.DefaultIndex)
				.SniffOnStartup()
				.SetBasicAuthentication("mpdreamz", "blahblah")
				.ExposeRawResponse()
				.SetPingTimeout(1000)
				.SetTimeout(2000);
			var client = new ElasticClient(settings, new HttpConnection(settings));

			var results = client.NodesInfo();
			results.IsValid.Should().BeTrue("{0}", results.ConnectionStatus.ToString());
			results.ConnectionStatus.NumberOfRetries.Should().Be(0);
			var uri = new Uri(results.ConnectionStatus.RequestUrl);
			uri.Port.Should().Be(9201);
			uri.Scheme.Should().Be("https");

			results = client.NodesInfo();
			results.IsValid.Should().BeTrue("{0}", results.ConnectionStatus.ToString());
			results.ConnectionStatus.NumberOfRetries.Should().Be(0);
			uri = new Uri(results.ConnectionStatus.RequestUrl);
			uri.Port.Should().Be(9200);
			uri.Scheme.Should().Be("https");
		}
	}
}
// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.Sdk;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Diagnostics;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Client.Settings;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using Xunit;

namespace Tests.ClientConcepts.Troubleshooting
{
	/**
	 * === Debug information
	 *
	 * Every response from Elasticsearch.Net and NEST contains a `DebugInformation` property
	 * that provides a human readable description of what happened during the request for both successful and
	 * failed requests
	 */
	public class DebugInformation : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public DebugInformation(ReadOnlyCluster cluster) : base(cluster) {}

		[I] public void DefaultDebug()
		{
			// hide
			var client = this.Client;

			var response = client.Search<Project>(s => s
				.Query(q => q
					.MatchAll()
				)
			);

			response.DebugInformation.Should().Contain("Valid NEST response");
		}
		//hide
		[U] public void PasswordIsNotExposedInDebugInformation()
		{
			// hide
			var client = new ElasticClient(new AlwaysInMemoryConnectionSettings()
				.DefaultIndex("index")
				.BasicAuthentication("user1", "pass2")
			);

			var response = client.Search<Project>(s => s
				.Query(q => q
					.MatchAll()
				)
			);

			response.DebugInformation.Should().NotContain("pass2");
		}
		//hide
		[U] public void ApiKeyIsNotExposedInDebugInformation()
		{
			// hide
			var client = new ElasticClient(new AlwaysInMemoryConnectionSettings()
				.DefaultIndex("index")
				.ApiKeyAuthentication("id1", "api_key1")
			);

			var response = client.Search<Project>(s => s
				.Query(q => q
					.MatchAll()
				)
			);

			response.DebugInformation.Should().NotContain("api_key1");
		}

		//hide
		[U] public void PasswordIsNotExposedInDebugInformationWhenPartOfUrl()
		{
			// hide
			var pool = new SingleNodeConnectionPool(new Uri("http://user1:pass2@localhost:9200"));
			var client = new ElasticClient(new ConnectionSettings(pool, new InMemoryConnection())
				.DefaultIndex("index")
			);

			var response = client.Search<Project>(s => s
				.Query(q => q
					.MatchAll()
				)
			);

			response.DebugInformation.Should().NotContain("pass2");
		}
		/**
		 * This can be useful in tracking down numerous problems and can also be useful when filing an
		 * {github}/issues[issue] on the GitHub repository.
		 *
		 * ==== Request and response bytes
		 *
		 * By default, the request and response bytes are not available within the debug information, but
		 * can be enabled globally on Connection Settings by setting `DisableDirectStreaming`. This
		 * disables direct streaming of
		 *
		 * . the serialized request type to the request stream
		 * . the response stream to a deserialized response type
		 */
		public void DisableDirectStreaming()
		{
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var settings = new ConnectionSettings(connectionPool)
				.DisableDirectStreaming(); // <1> disable direct streaming for *all* requests

			var client = new ElasticClient(settings);
		}

		/**
		 * or on a _per request_ basis
		 */
		[I] public void DisableDirectStreamingPerRequest()
		{
			// hide
			var client = TestClient.DefaultInMemoryClient;

			var response = client.Search<Project>(s => s
				.RequestConfiguration(r => r
					.DisableDirectStreaming() // <1> disable direct streaming for *this* request only
				)
				.Query(q => q
					.MatchAll()
				)
			);

			// hide
			response.DebugInformation.Should().Contain("\"match_all\":");
		}

		/**
		 * Configuring `DisableDirectStreaming` on an individual request takes precedence over
		 * any global configuration.
		 *
		 * There is typically a performance and allocation cost associated with disabling direct streaming
		 * since both the request and response bytes must be buffered in memory, to allow them to be
		 * exposed on the response call details.
		 *
		 * ==== TCP statistics
		 *
		 * It can often be useful to see the statistics for active TCP connections, particularly when
		 * trying to diagnose issues with the client. The client can collect the states of active TCP
		 * connections just before making a request, and expose these on the response and in the debug
		 * information.
		 *
		 * Similarly to `DisableDirectStreaming`, TCP statistics can be collected for every request
		 * by configuring on `ConnectionSettings`
		 */
		public void ConnectionSettingsTcpStats()
		{
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var settings = new ConnectionSettings(connectionPool)
				.EnableTcpStats(); // <1> collect TCP statistics for *all* requests

			var client = new ElasticClient(settings);
		}

		/**
		 * or on a _per request_ basis
		 */
		[I] public void RequestConfigurationTcpStats()
		{
			// hide
			var client = this.Client;

			var response = client.Search<Project>(s => s
				.RequestConfiguration(r => r
					.EnableTcpStats() // <1> collect TCP statistics for *this* request only
				)
				.Query(q => q
					.MatchAll()
				)
			);

			var debugInformation = response.DebugInformation;

			// hide
			debugInformation.Should().Contain("TCP states:");
		}

		/**
		 * With `EnableTcpStats` set, the states of active TCP connections will now be included
		 * on the response and in the debug information.
		 *
		 * The client includes a `TcpStats`
		 * class to help with retrieving more detail about active TCP connections should it be
		 * required
		 */
		[I] public void TcpStatistics()
		{
			// hide
			var client = this.Client;

			var tcpStatistics = TcpStats.GetActiveTcpConnections(); // <1> Retrieve details about active TCP connections, including local and remote addresses and ports
			var ipv4Stats = TcpStats.GetTcpStatistics(NetworkInterfaceComponent.IPv4); // <2> Retrieve statistics about IPv4
			var ipv6Stats = TcpStats.GetTcpStatistics(NetworkInterfaceComponent.IPv6); // <3> Retrieve statistics about IPv6

			var response = client.Search<Project>(s => s
				.Query(q => q
					.MatchAll()
				)
			);
		}

		 /**
		 * [NOTE]
		 * --
		 * Collecting TCP statistics may not be accessible in all environments, for example, Azure App Services.
		 * When this is the case, `TcpStats.GetActiveTcpConnections()` returns `null`.
		 * --
		 * 
		 * ==== ThreadPool statistics
         *
		 * It can often be useful to see the statistics for thread pool threads, particularly when
		 * trying to diagnose issues with the client. The client can collect statistics for both
		 * worker threads and asynchronous I/O threads, and expose these on the response and
		 * in debug information.
		 *
		 * Similar to collecting TCP statistics, ThreadPool statistics can be collected for all requests
		 * by configuring `EnableThreadPoolStats` on `ConnectionSettings`
		 */
		 public void ConnectionSettingsThreadPoolStats()
		 {
			 var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			 var settings = new ConnectionSettings(connectionPool)
				 .EnableThreadPoolStats(); // <1> collect thread pool statistics for *all* requests

			 var client = new ElasticClient(settings);
		 }

		 /**
		 * or on a _per request_ basis
		 */
		 [I] public void RequestConfigurationThreadPoolStats()
		 {
			 // hide
			 var client = this.Client;

			 var response = client.Search<Project>(s => s
				 .RequestConfiguration(r => r
						 .EnableThreadPoolStats() // <1> collect thread pool statistics for *this* request only
				 )
				 .Query(q => q
					 .MatchAll()
				 )
			 );

			 var debugInformation = response.DebugInformation; // <2> contains thread pool statistics

			 // hide
			 debugInformation.Should().Contain("ThreadPool statistics:");
		 }
		 /**
		 * With `EnableThreadPoolStats` set, the statistics of thread pool threads will now be included
		 * on the response and in the debug information.
		 */
	}
}

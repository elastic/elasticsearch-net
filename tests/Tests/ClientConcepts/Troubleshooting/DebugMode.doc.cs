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
	 * === Debug mode
	 *
	 * The <<debug-information, Debug information>> explains that every response from Elasticsearch.Net
	 * and NEST contains a `DebugInformation` property, and properties on `ConnectionSettings` and
	 * `RequestConfiguration` can control which additional information is included in debug information,
	 * for all requests or on a per request basis, respectively.
	 *
	 * During development, it can be useful to enable the most verbose debug information, to help
	 * identify and troubleshoot problems, or simply ensure that the client is behaving as expected.
	 * The `EnableDebugMode` setting on `ConnectionSettings` is a convenient shorthand for enabling
	 * verbose debug information, configuring a number of settings like
	 *
	 * * disabling direct streaming to capture request and response bytes
	 * * prettyfying JSON responses from Elasticsearch
	 * * collecting TCP statistics when a request is made
	 * * collecting thread pool statistics when a request is made
	 * * including the Elasticsearch stack trace in the response if there is a an error on the server side
	 */
	public class DebugMode : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public DebugMode(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void EnableDebugMode()
		{
			IConnectionPool pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			// hide
			pool = new StaticConnectionPool(Cluster.NodesUris());

			var settings = new ConnectionSettings(pool)
				.EnableDebugMode(); // <1> configure debug mode

			// hide
			settings.DefaultIndex(Client.ConnectionSettings.DefaultIndex);

			var client = new ElasticClient(settings);

			var response = client.Search<Project>(s => s
				.Query(q => q
					.MatchAll()
				)
			);

			var debugInformation = response.DebugInformation; // <2> verbose debug information

			// hide
			{
				debugInformation.Should().Contain("TCP states:");
				debugInformation.Should().Contain("ThreadPool statistics:");
			}
		}

		/**
		 * In addition to exposing debug information on the response, debug mode will also cause the debug
		 * information to be written to the trace listeners in the `System.Diagnostics.Debug.Listeners` collection
		 * by default, when the request has completed. A delegate can be passed when enabling debug mode to perform
		 * a different action when a request has completed, using <<logging-with-on-request-completed, `OnRequestCompleted`>>
		 */
		public void DebugModeOnRequestCompleted()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var client = new ElasticClient(new ConnectionSettings(pool)
				.EnableDebugMode(apiCallDetails =>
				{
					// do something with the call details e.g. send with logging framework
				})
			);
		}
	}

}

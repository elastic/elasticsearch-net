// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Elasticsearch.Net;
using Nest;
using Tests.Domain;

namespace Tests.ClientConcepts.Connection
{
	public class ConfigurationOptions
	{
		/**[[configuration-options]]
		 * === Configuration options
		 *
		 * Connecting to Elasticsearch with <<elasticsearch-net-getting-started,Elasticsearch.Net>> and <<nest-getting-started,NEST>> is easy, but
		 * it's entirely possible that you'd like to change the default connection behaviour. There are a number of configuration options available
		 * on `ConnectionConfiguration` for the low level client and `ConnectionSettings` for the high level client that can be used to control
		 * how the clients interact with Elasticsearch.
		 *
		 * ==== Options on ConnectionConfiguration
		 *
		 * The following is a list of available connection configuration options on `ConnectionConfiguration`; since
		 * `ConnectionSettings` derives from `ConnectionConfiguration`, these options are available for both
		 * the low level and high level client:
		 *
		 * :xml-docs: Elastic.Transport:TransportConfigurationBase`1
		 *
		 * ==== ConnectionConfiguration with ElasticLowLevelClient
		 *
		 * Here's an example to demonstrate setting several configuration options using the low level client
		 */
		public void AvailableOptions()
		{
			var connectionConfiguration = new ConnectionConfiguration()
				.DisableAutomaticProxyDetection()
				.EnableHttpCompression()
				.DisableDirectStreaming()
				.PrettyJson()
				.RequestTimeout(TimeSpan.FromMinutes(2));

			var lowLevelClient = new ElasticLowLevelClient(connectionConfiguration);

			/**
			 * ==== Options on ConnectionSettings
			 *
			 * The following is a list of available connection configuration options on `ConnectionSettings`:
			 *
			 * :xml-docs: Nest:ConnectionSettingsBase`1
			 *
			 * ==== ConnectionSettings with ElasticClient
			 *
			 * Here's an example to demonstrate setting several configuration options using the high level client
			 */
			var connectionSettings = new ConnectionSettings()
				.DefaultMappingFor<Project>(i => i
					.IndexName("my-projects")
					.IdProperty(p => p.Name)
				)
				.EnableDebugMode()
				.PrettyJson()
				.RequestTimeout(TimeSpan.FromMinutes(2));

			var client = new ElasticClient(connectionSettings);

			/**[NOTE]
			* ====
			*
			* Basic Authentication credentials can alternatively be specified on the node URI directly
			*/
			var uri = new Uri("http://username:password@localhost:9200");
			var settings = new ConnectionConfiguration(uri);
		}
		/**
		* but this can be awkward when using connection pooling with multiple nodes, especially when the connection pool
		* used is one that is capable of reseeding itself. For this reason, we'd recommend specifying credentials
		* on `ConnectionSettings`.
		*====
		*/
	}
}

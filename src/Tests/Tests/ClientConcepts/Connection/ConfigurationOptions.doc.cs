using System;
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
		 * on `ConnectionSettings` (and `ConnectionConfiguration` for Elasticsearch.Net) that can be used to control
		 * how the clients interact with Elasticsearch.
         *
         * ==== Options on ConnectionConfiguration
         *
         * The following is a list of available connection configuration options on `ConnectionConfiguration`; since
         * `ConnectionSettings` derives from `ConnectionConfiguration`, these options are available for both
         * Elasticsearch.Net and NEST:
         *
         * :xml-docs: Elasticsearch.Net:ConnectionConfiguration`1
         *
         * ==== Options on ConnectionSettings
         *
         * The following is a list of available connection configuration options on `ConnectionSettings`:
         *
         * :xml-docs: Nest:ConnectionSettingsBase`1
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
			 * And with the high level client
			 */
			var connectionSettings = new ConnectionSettings()
				.DefaultMappingFor<Project>(i => i
					.IndexName("my-projects")
					.TypeName("project")
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
		* used is one that is capable of reseeding iteslf. For this reason, we'd recommend specifying credentials
		* on `ConnectionSettings`.
        *====
        */
	}
}

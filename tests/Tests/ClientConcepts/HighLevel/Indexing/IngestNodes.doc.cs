// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Nest;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using static Elastic.Transport.Products.Elasticsearch.ElasticsearchNodeFeatures;

namespace Tests.ClientConcepts.HighLevel.Indexing
{
	/**[[ingest-nodes]]
	*=== Ingest Node
	*
	* Elasticsearch will automatically re-route index requests to ingest nodes,
	* however with some careful consideration you can optimise this path.
	*/
	public class IngestNodes : DocumentationTestBase
	{
		private readonly IElasticClient client = new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection()));

		/**
		* ==== Custom indexing client
		*
		* Since Elasticsearch will automatically reroute ingest requests to ingest nodes, you don't have to specify or configure any routing
		* information. However, if you're doing heavy ingestion and have dedicated ingest nodes, it makes sense to send index requests to
		* these nodes directly, to avoid any extra hops in the cluster.
		*
		* The simplest way to achieve this is to create a dedicated "indexing" client instance, and use it for indexing requests.
		*/
		public void CustomClient()
		{
			var pool = new StaticConnectionPool(new [] //<1> list of ingest nodes
			{
				new Uri("http://ingestnode1:9200"),
				new Uri("http://ingestnode2:9200"),
				new Uri("http://ingestnode3:9200")
			});
			var settings = new ConnectionSettings(pool);
			var indexingClient = new ElasticClient(settings);
		}

		/**
		* ==== Determining ingest capability
		*
		* In complex cluster configurations it can be easier to use a sniffing connection pool along with a node predicate to
		* filter out the nodes that have ingest capabilities. This allows you to customise the cluster and not have to reconfigure
		* the client.
		*/
		public void SniffingConnectionPool()
		{
			var pool = new SniffingConnectionPool(new [] //<1> list of cluster nodes
			{
				new Uri("http://node1:9200"),
				new Uri("http://node2:9200"),
				new Uri("http://node3:9200")
			});
			var settings = new ConnectionSettings(pool).NodePredicate(n => n.HasFeature(IngestEnabled)); //<2> predicate to select only nodes with ingest capabilities
			var indexingClient = new ElasticClient(settings);
		}
	}
}

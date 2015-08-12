using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;
using Elasticsearch.Net.Providers;
using FluentAssertions;
using Tests.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Tests.ClientConcepts.LowLevel
{
	public class ConnectionPooling
	{
		/** = Connection Pooling
		 * Connection pooling is the internal mechanism that takes care of registering what nodes there are in the cluster and which
		 * we can use to issue client calls on.
		 */
		
		/** == SingleNodeConnectionPool 
		* The simplest of all connection pools, this takes a single `Uri` and uses that to connect to elasticsearch for all the calls
		* It doesn't opt in to sniffing and pinging behavior, and will never mark nodes dead or alive. The one `Uri` it holds is always
		* ready to go. 
		*/
		[U] public void SingleNode()
		{
			var uri = new Uri("http://localhost:9201");
			var pool = new SingleNodeConnectionPool(uri);
			pool.Nodes.Should().HaveCount(1);
			var node = pool.Nodes.First();
			node.Uri.Port.Should().Be(9201);

			/** This type of pool is hardwired to optout out sniffing*/
			pool.SupportsReseeding.Should().BeFalse();
			/** and pinging */
			pool.SupportsPinging.Should().BeFalse();

			/** When you use the low ceremony ElasticClient constructor that takes a single Uri
			* We default to this SingleNodeConnectionPool */
			var client = new ElasticClient(uri);
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SingleNodeConnectionPool>();

			/** However we urge that you always pass your connection settings explicitly */
			client = new ElasticClient(new ConnectionSettings(uri));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SingleNodeConnectionPool>();

			/** or even better pass the connection pool explicitly  */
			client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SingleNodeConnectionPool>();
		}

		/** == StaticConnectionPool 
		* The static connection pool is great if you have a known small sized cluster and do no want to enable 
		* sniffing to find out the cluster topology.
		*/
		[U] public void Static()
		{
			var uris = Enumerable.Range(9200, 5).Select(p => new Uri("http://localhost:" + p));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new StaticConnectionPool(uris);

			/** Or using an enumerable of `Node` */
			var nodes = uris.Select(u=>new Node(u));
			pool = new StaticConnectionPool(nodes);

			/** This type of pool is hardwirted to optout out sniffing*/
			pool.SupportsReseeding.Should().BeFalse();

			/** but supports pinging when enabled */
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using this static connection pool pass 
			* the connection pool to the connectionsettings you pass to ElasticClient
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<StaticConnectionPool>();
		}

		/** == SniffingConnectionPool 
		* A subclass of StaticConnectionPool that allows itself to be reseeded at run time.
		* It comes with a very minor overhead of a ReaderWriterLock to ensure thread safety.
		*/
		[U] public void Sniffing()
		{
			var uris = Enumerable.Range(9200, 5).Select(p => new Uri("http://localhost:" + p));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new SniffingConnectionPool(uris);

			/** Or using an enumerable of `Node`
			* A major benefit here is you can include known node roles when seeding 
			* NEST can use this information to favour sniffing on master eligable nodes first
			* and take master only nodes out of rotation for issueing client calls on.
			*/
			var nodes = uris.Select(u=>new Node(u));
			pool = new SniffingConnectionPool(nodes);

			/** This type of pool is hardwirted to optout out sniffing*/
			pool.SupportsReseeding.Should().BeTrue();

			/** but supports pinging when enabled */
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using this siffing connection pool pass 
			* the connection pool to the connectionsettings you pass to ElasticClient
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SniffingConnectionPool>();
		}


	}
}

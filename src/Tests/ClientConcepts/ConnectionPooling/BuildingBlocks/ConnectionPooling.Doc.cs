using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	public class ConnectionPooling
	{
		/**== Connection Pooling
		 * Connection pooling is the internal mechanism that takes care of registering what nodes there are in the cluster and which
		 * NEST can use to issue client calls on. There are four types of connection pool
		 *
		 * - <<single-node-connection-pool,SingleNodeConnectionPool>>
		 * - <<static-connection-pool,StaticConnectionPool>>
		 * - <<sniffing-connection-pool,SniffingConnectionPool>>
		 * - <<sticky-connection-pool,StickyConnectionPool>>
		 */

		/**
		* [[single-node-connection-pool]]
		* === SingleNodeConnectionPool
		* The simplest of all connection pools, this takes a single `Uri` and uses that to connect to Elasticsearch for all the calls
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

			/** This type of pool is hardwired to opt out of reseeding (and hence sniffing)*/
			pool.SupportsReseeding.Should().BeFalse();
			/** and pinging */
			pool.SupportsPinging.Should().BeFalse();

			/** When you use the low ceremony `ElasticClient` constructor that takes a single `Uri`,
			* We default to using `SingleNodeConnectionPool` */
			var client = new ElasticClient(uri);
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SingleNodeConnectionPool>();

			/** However we urge that you always pass your connection settings explicitly */
			client = new ElasticClient(new ConnectionSettings(uri));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SingleNodeConnectionPool>();

			/** or even better pass the connection pool explicitly  */
			client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SingleNodeConnectionPool>();
		}

		/**[[static-connection-pool]]
		* === StaticConnectionPool
		* The static connection pool is great if you have a known small sized cluster and do no want to enable
		* sniffing to find out the cluster topology.
		*/
		[U] public void Static()
		{
			var uris = Enumerable.Range(9200, 5).Select(p => new Uri("http://localhost:" + p));

			/** a connection pool can be seeded using an enumerable of `Uri`s */
			var pool = new StaticConnectionPool(uris);

			/** Or using an enumerable of `Node`s */
			var nodes = uris.Select(u => new Node(u));
			pool = new StaticConnectionPool(nodes);

			/** This type of pool is hardwired to opt out of reseeding (and hence sniffing)*/
			pool.SupportsReseeding.Should().BeFalse();

			/** but supports pinging when enabled */
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using this static connection pool, pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<StaticConnectionPool>();
		}

		/**[[sniffing-connection-pool]]
		* === SniffingConnectionPool
		* A subclass of `StaticConnectionPool` that allows itself to be reseeded at run time.
		* It comes with a very minor overhead of a `ReaderWriterLockSlim` to ensure thread safety.
		*/
		[U] public void Sniffing()
		{
			var uris = Enumerable.Range(9200, 5).Select(p => new Uri("http://localhost:" + p));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new SniffingConnectionPool(uris);

			/** Or using an enumerable of `Node`s.
			* A major benefit here is you can include known node roles when seeding and
			* NEST can use this information to favour sniffing on master eligible nodes first
			* and take master only nodes out of rotation for issuing client calls on.
			*/
			var nodes = uris.Select(u=>new Node(u));
			pool = new SniffingConnectionPool(nodes);

			/** This type of pool is hardwired to opt in to reseeding (and hence sniffing)*/
			pool.SupportsReseeding.Should().BeTrue();

			/** and pinging */
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using the sniffing connection pool pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<SniffingConnectionPool>();
		}

		/**[[sticky-connection-pool]]
		* === StickyConnectionPool
		* A type of `IConnectionPool` that returns the first live node such that it is sticky between
		* requests.
		* It uses https://msdn.microsoft.com/en-us/library/system.threading.interlocked(v=vs.110).aspx[`System.Threading.Interlocked`]
		* to keep an _indexer_ to the last live node in a thread safe manner.
		*/
		[U] public void Sticky()
		{
			var uris = Enumerable.Range(9200, 5).Select(p => new Uri("http://localhost:" + p));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new StickyConnectionPool(uris);

			/** Or using an enumerable of `Node`.
			* A major benefit here is you can include known node roles when seeding and
			* NEST can use this information to favour sniffing on master eligible nodes first
			* and take master only nodes out of rotation for issuing client calls on.
			*/
			var nodes = uris.Select(u=>new Node(u));
			pool = new StickyConnectionPool(nodes);

			/** This type of pool is hardwired to opt out of reseeding (and hence sniffing)*/
			pool.SupportsReseeding.Should().BeFalse();

			/** but does support pinging */
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using the sticky connection pool pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool.Should().BeOfType<StickyConnectionPool>();
		}
	}
}

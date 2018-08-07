using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	public class ConnectionPooling
	{
		/**[[connection-pooling]]
		 * === Connection pools
		 * Connection pooling is the internal mechanism that takes care of registering what nodes there are in the cluster and which
		 * NEST can use to issue client calls on.
         *
         * [IMPORTANT]
         * --
         * Despite the name, a connection pool in NEST is **not** like connection pooling that you may be familiar with from
         * https://msdn.microsoft.com/en-us/library/bb399543(v=vs.110).aspx[interacting with a database using ADO.Net]; for example,
         * a connection pool in NEST is **not** responsible for managing an underlying pool of TCP connections to Elasticsearch,
         * this is https://blogs.msdn.microsoft.com/adarshk/2005/01/02/understanding-system-net-connection-management-and-servicepointmanager/[handled by the ServicePointManager in Desktop CLR].
         * --
         *
         * So, what is a connection pool in NEST responsible for? It is responsible for managing the nodes in an Elasticsearch
         * cluster to which a connection can be made and there is one instance of an `IConnectionPool` associated with an
         * instance of `ConnectionSettings`. Since a <<lifetimes,single client and connection settings instance is recommended for the
         * life of the application>>, the lifetime of a single connection pool instance will also be bound to the lifetime
         * of the application.
         *
         * There are four types of connection pool
		 *
		 * - <<single-node-connection-pool,SingleNodeConnectionPool>>
		 * - <<static-connection-pool,StaticConnectionPool>>
		 * - <<sniffing-connection-pool,SniffingConnectionPool>>
		 * - <<sticky-connection-pool,StickyConnectionPool>>
		 */

		/**
		* [[single-node-connection-pool]]
		* ==== SingleNodeConnectionPool
		*
		* The simplest of all connection pools and the default if no connection pool is explicitly passed to the `ConnectionSettings` constructor.
		* It takes a single `Uri` and uses that to connect to Elasticsearch for all the calls. Single node connection pool doesn't opt in to
		* sniffing or pinging behavior and will never mark nodes dead or alive. The one `Uri` it holds is always ready to go.
		*
		* Single node connection pool is the pool to use if your cluster contains only a single node or you are interacting with
		* your cluster through a single load balancer instance.
		*/
		[U] public void SingleNode()
		{
			var uri = new Uri("http://localhost:9201");
			var pool = new SingleNodeConnectionPool(uri);
			pool.Nodes.Should().HaveCount(1);
			var node = pool.Nodes.First();
			node.Uri.Port.Should().Be(9201);

			/** This type of pool is hardwired to opt out of reseeding (thus, sniffing) as well as pinging */
			pool.SupportsReseeding.Should().BeFalse();
			pool.SupportsPinging.Should().BeFalse();

			/** When you use the low ceremony `ElasticClient` constructor that takes a single `Uri`,
			* internally a `SingleNodeConnectionPool` is used
			*/
			var client = new ElasticClient(uri);
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<SingleNodeConnectionPool>();

			/** However we urge that you always pass your connection settings explicitly
			 */
			client = new ElasticClient(new ConnectionSettings(uri));
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<SingleNodeConnectionPool>();

			/** or even better pass the connection pool explicitly
			 */
			client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<SingleNodeConnectionPool>();
		}

		/**[[static-connection-pool]]
		* ==== StaticConnectionPool
		*
		* The static connection pool is great if you have a known small sized cluster and do no want to enable
		* sniffing to find out the cluster topology.
		*/
		[U] public void Static()
		{
			var uris = Enumerable.Range(9200, 5)
				.Select(port => new Uri($"http://localhost:{port}"));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new StaticConnectionPool(uris);

			/** Or using an enumerable of `Node` */
			var nodes = uris.Select(u => new Node(u));
			pool = new StaticConnectionPool(nodes);

			/** This type of pool is hardwired to opt out of reseeding
			 * (and hence sniffing) but supports pinging when enabled
			 */
			pool.SupportsReseeding.Should().BeFalse();
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using the static connection pool, pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<StaticConnectionPool>();
		}

		/**[[sniffing-connection-pool]]
		* ==== SniffingConnectionPool
		*
		* A pool derived from `StaticConnectionPool`, a sniffing connection pool allows itself to be reseeded at run time.
		* It comes with the very minor overhead of a `ReaderWriterLockSlim` to ensure thread safety.
		*/
		[U] public void Sniffing()
		{
			var uris = Enumerable.Range(9200, 5)
				.Select(port => new Uri($"http://localhost:{port}"));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new SniffingConnectionPool(uris);

			/** Or using an enumerable of `Node`. A major benefit in using nodes is that you can include
			* known node roles when seeding which
			* NEST can use to favour sniffing on master eligible nodes first,
			* and take master only nodes out of rotation for issuing client calls on.
			*/
			var nodes = uris.Select(u=>new Node(u));
			pool = new SniffingConnectionPool(nodes);

			/** This type of pool is hardwired to opt in to reseeding (and hence sniffing), and pinging */
			pool.SupportsReseeding.Should().BeTrue();
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using the sniffing connection pool pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<SniffingConnectionPool>();
		}

		/**[[sticky-connection-pool]]
		* ==== StickyConnectionPool
		*
		* A type of connection pool that returns the first live node to issue a request against, such that the node is _sticky_ between requests.
		* It uses https://msdn.microsoft.com/en-us/library/system.threading.interlocked(v=vs.110).aspx[`System.Threading.Interlocked`]
		* to keep an _indexer_ to the last live node in a thread safe manner.
		*/
		[U] public void Sticky()
		{
			var uris = Enumerable.Range(9200, 5)
				.Select(port => new Uri($"http://localhost:{port}"));

			/** a connection pool can be seeded using an enumerable of `Uri` */
			var pool = new StickyConnectionPool(uris);

			/** Or using an enumerable of `Node`.
			* A major benefit here is you can include known node roles when seeding and
			* NEST can use this information to favour sniffing on master eligible nodes first
			* and take master only nodes out of rotation for issuing client calls on.
			*/
			var nodes = uris.Select(u=>new Node(u));
			pool = new StickyConnectionPool(nodes);

			/** This type of pool is hardwired to opt out of reseeding (and hence sniffing), but does support sniffing*/
			pool.SupportsReseeding.Should().BeFalse();
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using the sticky connection pool pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<StickyConnectionPool>();
		}

		/**[[sticky-sniffing-connection-pool]]
		* ==== Sticky Sniffing Connection Pool
		*
		* A type of connection pool that returns the first live node to issue a request against, such that the node is _sticky_ between requests.
		* This implementation supports sniffing and sorting so that each instance of your application can favor a node in the same rack based
		* on node attributes for instance.
		*/
		[U] public void SniffingSortedSticky()
		{
			var uris = Enumerable.Range(9200, 5)
				.Select(port => new Uri($"http://localhost:{port}"));

			/** a sniffing sorted sticky pool takes a second parameter `Func` takes a Node and returns a weight.
			* Nodes will be sorted descending by weight. In the following example we score nodes that are client nodes
			* AND in rack_id `rack_one` the highest
			*/

			var pool = new StickySniffingConnectionPool(uris, n =>
				(n.ClientNode ? 10 : 0)
				+ (n.Settings.TryGetValue("node.attr.rack_id", out var rackId)
				   		&& rackId.ToString() == "rack_one" ? 10 : 0));

			pool.SupportsReseeding.Should().BeTrue();
			pool.SupportsPinging.Should().BeTrue();

			/** To create a client using the sticky sniffing connection pool pass
			* the connection pool to the `ConnectionSettings` you pass to `ElasticClient`
			*/
			var client = new ElasticClient(new ConnectionSettings(pool));
			client.ConnectionSettings.ConnectionPool
				.Should().BeOfType<StickySniffingConnectionPool>();
		}
	}
}

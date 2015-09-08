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
using System.Threading;

namespace Tests.ClientConcepts.LowLevel
{
	public class SkippingDeadNodes
	{
		/** Round Robin - Skipping Dead Nodes
		 * When selecting nodes the connection pool will try and skip all the nodes that are marked dead.
		 */

		/** == GetNext
		* GetNext is implemented in a lock free thread safe fashion, meaning each callee gets returned its own cursor to advance
		* over the internal list of nodes. This to guarantee each request that needs to fall over tries all the nodes without
		* suffering from noisy neighboors advancing a global cursor.
		*/

		protected int NumberOfNodes = 3;

		[U] public void EachViewSeesNextButSkipsTheDeadNode()
		{
			var seeds = Enumerable.Range(9200, NumberOfNodes).Select(p => new Node(new Uri("http://localhost:" + p))).ToList();
			seeds.First().MarkDead(DateTime.Now.AddDays(1));
			var pool = new StaticConnectionPool(seeds, randomize: false);
			for (var i = 0; i < 20; i++)
			{
				var node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9201);
				node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9202);
			}
			/** After we marke the first node alive again we expect it to be hit again*/
			seeds.First().MarkAlive();
			for (var i = 0; i < 20; i++)
			{
				var node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9201);
				node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9202);
				node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9200);
			}
		}
		[U] public void ViewSeesResurrectedNodes()
		{
			var dateTimeProvider = new TestableDateTimeProvider();
			var seeds = Enumerable.Range(9200, NumberOfNodes).Select(p => new Node(new Uri("http://localhost:" + p))).ToList();
			seeds.First().MarkDead(dateTimeProvider.Now().AddDays(1));
			var pool = new StaticConnectionPool(seeds, randomize: false, dateTimeProvider: dateTimeProvider);
			for (var i = 0; i < 20; i++)
			{
				var node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9201);
				node = pool.CreateView().First();
				node.Uri.Port.Should().Be(9202);
			}
			/** If we forward our clock 2 days the node that was marked dead until tomorrow (or yesterday!) should be resurrected */
			dateTimeProvider.ChangeTime(d => d.AddDays(2));
			var n = pool.CreateView().First();
			n.Uri.Port.Should().Be(9201);
			n = pool.CreateView().First();
			n.Uri.Port.Should().Be(9202);
			n = pool.CreateView().First();
			n.Uri.Port.Should().Be(9200);
			n.IsResurrected.Should().BeTrue();
		}
	}
}

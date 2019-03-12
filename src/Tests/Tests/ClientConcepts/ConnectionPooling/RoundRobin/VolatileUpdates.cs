using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Tests.ClientConcepts.ConnectionPooling.RoundRobin
{
	public class VolatileUpdates
	{
		protected int NumberOfNodes = 10;
		private readonly Random Random = new Random();

		private readonly List<Node> Update = Enumerable.Range(9200, 10)
			.Select(p => new Uri("http://localhost:" + p))
			.Select(u => new Node(u))
			.ToList();

		[U] public void SniffingPoolWithstandsConcurrentReadAndWrites()
		{
			var uris = Enumerable.Range(9200, NumberOfNodes).Select(p => new Uri("http://localhost:" + p));
			var sniffingPool = new SniffingConnectionPool(uris, false);

			Action callSniffing = () => AssertCreateView(sniffingPool);

			callSniffing.Should().NotThrow();
		}

		[U] public void StaticPoolWithstandsConcurrentReadAndWrites()
		{
			var uris = Enumerable.Range(9200, NumberOfNodes).Select(p => new Uri("http://localhost:" + p));
			var staticPool = new StaticConnectionPool(uris, false);

			Action callStatic = () => AssertCreateView(staticPool);

			callStatic.Should().NotThrow();
		}

		// hide
		public void AssertCreateView(IConnectionPool pool)
		{
			/**
			*/
			var threads = Enumerable.Range(0, 50)
				.Select(i => CreateReadAndUpdateThread(pool))
				.ToList();

			foreach (var t in threads) t.Start();
			foreach (var t in threads) t.Join();
		}

		//hide
		public Thread CreateReadAndUpdateThread(IConnectionPool pool) => new Thread(() =>
		{
			for (var i = 0; i < 1000; i++)
			{
				foreach (var v in CallGetNext(pool))
				{
					if (Random.Next(10) % 2 == 0) pool.Reseed(Update);
				}
			}
		});

		//hide
		private IEnumerable<int> CallGetNext(IConnectionPool pool)
		{
			foreach (var n in pool.CreateView()) yield return n.Uri.Port;
		}
	}
}

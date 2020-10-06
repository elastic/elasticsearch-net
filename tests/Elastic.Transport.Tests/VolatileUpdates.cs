// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace Elastic.Transport.Tests
{
	public class VolatileUpdates
	{
		private readonly int _numberOfNodes = 10;
		private readonly Random _random = new Random();

		private readonly List<Node> _update = Enumerable.Range(9200, 10)
			.Select(p => new Uri("http://localhost:" + p))
			.Select(u => new Node(u))
			.ToList();

		[Fact] public void SniffingPoolWithstandsConcurrentReadAndWrites()
		{
			var uris = Enumerable.Range(9200, _numberOfNodes).Select(p => new Uri("http://localhost:" + p));
			var sniffingPool = new SniffingConnectionPool(uris, false);

			Action callSniffing = () => AssertCreateView(sniffingPool);

			callSniffing.Should().NotThrow();
		}

		[Fact] public void StaticPoolWithstandsConcurrentReadAndWrites()
		{
			var uris = Enumerable.Range(9200, _numberOfNodes).Select(p => new Uri("http://localhost:" + p));
			var staticPool = new StaticConnectionPool(uris, false);

			Action callStatic = () => AssertCreateView(staticPool);

			callStatic.Should().NotThrow();
		}

		private void AssertCreateView(IConnectionPool pool)
		{
			var threads = Enumerable.Range(0, 50)
				.Select(i => CreateReadAndUpdateThread(pool))
				.ToList();

			foreach (var t in threads) t.Start();
			foreach (var t in threads) t.Join();
		}

		private Thread CreateReadAndUpdateThread(IConnectionPool pool) => new Thread(() =>
		{
			for (var i = 0; i < 1000; i++)
			{
				foreach (var _ in CallGetNext(pool))
				{
					if (_random.Next(10) % 2 == 0) pool.Reseed(_update);
				}
			}
		});

		private IEnumerable<int> CallGetNext(IConnectionPool pool)
		{
			foreach (var n in pool.CreateView()) yield return n.Uri.Port;
		}
	}
}

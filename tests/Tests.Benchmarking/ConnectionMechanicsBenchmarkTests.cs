// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Elastic.Transport;
using Nest;
using Tests.Benchmarking.Framework;

namespace Tests.Benchmarking
{
	[BenchmarkConfig]
	public class ConnectionMechanicsBenchmarkTests
	{
		private IElasticClient Client { get; set; }
		private PostData Post { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			Post = PostData.String("{}");

			var nodes = Enumerable.Range(0, 100).Select(i => new Node(new Uri($"http://localhost:{i}"))).ToList();

			var connectionPool = new SniffingConnectionPool(nodes);
			var connection = new InMemoryConnection();
			var settings = new ConnectionSettings(connectionPool, connection)
				.SniffOnStartup(false)
				.SniffOnConnectionFault(false);
			Client = new ElasticClient(settings);

		}


		[Benchmark(Description = "SearchResponse", OperationsPerInvoke = 1000)]
		public void SearchResponse() => Client.LowLevel.Search<VoidResponse>("index", Post);
	}
}

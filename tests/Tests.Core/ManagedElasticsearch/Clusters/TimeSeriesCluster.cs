// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Nest;
using Tests.Domain;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class TimeSeriesCluster : XPackCluster
	{
		protected override void SeedNode() => new TimeSeriesSeeder(Client).SeedNode();
	}

	public class TimeSeriesSeeder
	{
		public static readonly string IndicesWildCard = "logs-*";
		private readonly IElasticClient _client;

		public TimeSeriesSeeder(IElasticClient client) => _client = client;

		public void SeedNode()
		{
			_client.Indices.PutTemplate("logs-template", p => p
				.Create()
				.Map<Log>(m => m.AutoMap())
				.IndexPatterns(IndicesWildCard)
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
				)
			);

			var logs = Log.Generator.GenerateLazy(200_000);
			var sw = Stopwatch.StartNew();
			var dropped = new List<Log>();
			var bulkAll = _client.BulkAll(new BulkAllRequest<Log>(logs)
			{
				Size = 10_000,
				MaxDegreeOfParallelism = 8,
				RefreshOnCompleted = true,
				RefreshIndices = IndicesWildCard,
				DroppedDocumentCallback = (d, l) => dropped.Add(l),
				BufferToBulk = (b, buffer) => b.IndexMany(buffer, (i, l) => i.Index($"logs-{l.Timestamp:yyyy-MM-dd}"))
			});
			bulkAll.Wait(TimeSpan.FromMinutes(1), delegate { });
			Console.WriteLine($"Completed in {sw.Elapsed} with {dropped.Count} dropped logs");

			var countResult = _client.Count<Log>(s => s.Index(IndicesWildCard));
			Console.WriteLine($"Stored {countResult.Count} in {IndicesWildCard} indices");
		}
	}
}

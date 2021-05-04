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

		protected override ConnectionSettings ConnectionSettings(ConnectionSettings s)
		{
			s.DefaultMappingFor<Log>(m => m.IndexName(TimeSeriesSeeder.IndicesWildCard));
			return base.ConnectionSettings(s);
		}
	}

	public class TimeSeriesSeeder
	{
		public static readonly string IndicesWildCard = "customlogs-*";
		private readonly IElasticClient _client;

		public TimeSeriesSeeder(IElasticClient client) => _client = client;

		public void SeedNode()
		{
			_client.Indices.PutTemplate("customlogs-template", p => p
				.Create()
				.Map<Log>(m => m
					.AutoMap()
					.Properties(p => p.Date(d => d.Name(n => n.Timestamp))))
				.IndexPatterns(IndicesWildCard)
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
				)
			);

			var logs = Log.Generator.GenerateLazy(100_000);
			var sw = Stopwatch.StartNew();
			var dropped = new List<Log>();
			var bulkAll = _client.BulkAll(new BulkAllRequest<Log>(logs)
			{
				Size = 10_000,
				MaxDegreeOfParallelism = 10,
				RefreshOnCompleted = true,
				RefreshIndices = IndicesWildCard,
				DroppedDocumentCallback = (d, l) => dropped.Add(l),
				BufferToBulk = (b, buffer) => b.IndexMany(buffer, (i, l) => i.Index($"customlogs-{l.Timestamp:yyyy-MM-dd}"))
			});
			bulkAll.Wait(TimeSpan.FromMinutes(1), delegate { });
			Console.WriteLine($"Completed in {sw.Elapsed} with {dropped.Count} dropped logs");

			var countResult = _client.Count<Log>(s => s.Index(IndicesWildCard));
			Console.WriteLine($"Stored {countResult.Count} in {IndicesWildCard} indices");
		}
	}
}

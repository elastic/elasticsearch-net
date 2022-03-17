// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Helpers;
using Elastic.Transport;
using Tests.Domain;

namespace Tests.Core.ManagedElasticsearch.NodeSeeders;

public class TimeSeriesSeeder
{
	public static readonly string IndicesWildCard = "customlogs-*";
	private readonly IElasticsearchClient _client;

	public TimeSeriesSeeder(IElasticsearchClient client) => _client = client;

	public void SeedNode()
	{
		var transport = _client.Transport;

		var req = new
		{
			mapping = new {
				properties = new Dictionary<string, object>
				{
					{ "timestamp", new { type = "date" } }
				}
			},
			index_patterns = new[] { IndicesWildCard },
			settings = new Dictionary<string, object>
				{
					{ "index.number_of_replicas", 0 },
					{ "index.number_of_shards", 1 }
				}
		};

		_ = transport.Request<BytesResponse>(HttpMethod.PUT, $"_template/customlogs-template", PostData.Serializable(req));

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
		bulkAll.Wait(TimeSpan.FromMinutes(1), delegate
		{ });
		Console.WriteLine($"Completed in {sw.Elapsed} with {dropped.Count} dropped logs");

		var countResult = _client.Count(s => s.Index(IndicesWildCard));
		Console.WriteLine($"Stored {countResult.Count} in {IndicesWildCard} indices");
	}
}

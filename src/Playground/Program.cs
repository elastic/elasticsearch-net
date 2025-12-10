// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

var baseOptions = new VectorIngestOptions
{
	DatasetFile = "C:\\Users\\Florian\\Desktop\\bench\\open_ai_corpus-initial-indexing-1k.json",
	Repetitions = 1,
	MaxDegreeOfParallelism = 1,
	WarmupPasses = 5,
	MeasuredPasses = 3,
	ElasticsearchEndpoint = new Uri("http://192.168.100.87:9200"),
	ElasticsearchUsername = "elastic",
	ElasticsearchPassword = "julCIvcZ"
};

var cases = new VectorIngestOptions[]
{
	baseOptions with { UseBase64VectorEncoding = false, ChunkSize = 100  },
	baseOptions with { UseBase64VectorEncoding = false, ChunkSize = 250  },
	baseOptions with { UseBase64VectorEncoding = false, ChunkSize = 500  },
	baseOptions with { UseBase64VectorEncoding = false, ChunkSize = 1000 },
	baseOptions with { UseBase64VectorEncoding = true , ChunkSize = 100  },
	baseOptions with { UseBase64VectorEncoding = true , ChunkSize = 250  },
	baseOptions with { UseBase64VectorEncoding = true , ChunkSize = 500  },
	baseOptions with { UseBase64VectorEncoding = true , ChunkSize = 1000 }
};

foreach (var testcase in cases)
{
	Console.Write($"Base64: {(testcase.UseBase64VectorEncoding ? '1' : '0')}, Chunk size: {testcase.ChunkSize,4} == ");
	await VectorIngest.Ingest(testcase);
}

public sealed record VectorIngestOptions
{
	/// <summary>
	/// The path to the dataset file.
	/// </summary>
	public required string DatasetFile { get; init; }

	/// <summary>
	/// The number of times the dataset is repeated.
	/// </summary>
	public int Repetitions { get; init; } = 1;

	/// <summary>
	/// The chunk size for the individual bulk requests.
	/// </summary>
	public int ChunkSize { get; init; } = 100;

	/// <summary>
	/// Configures whether vector data is encoded in <c>base64</c> format.
	/// </summary>
	public bool UseBase64VectorEncoding { get; init; } = true;

	/// <summary>
	/// The maximum number of concurrent bulk operations allowed when processing tasks in parallel.
	/// </summary>
	public int MaxDegreeOfParallelism { get; init; } = 1;

	/// <summary>
	/// The number of warmup passes to perform before measurements begin.
	/// </summary>
	public int WarmupPasses { get; init; } = 5;

	/// <summary>
	/// The number of measurement passes to perform.
	/// </summary>
	public int MeasuredPasses { get; init; } = 3;

	public required Uri ElasticsearchEndpoint { get; init; }
	public required string ElasticsearchUsername { get; init; }
	public required string ElasticsearchPassword { get; init; }
}

public static class VectorIngest
{
	public static async Task Ingest(VectorIngestOptions options)
	{
		var docs = LoadDataset(options.DatasetFile, options.Repetitions);

		var sw = new Stopwatch();
		var elapsedSeconds = 0.0d;

		var client = CreateClient(options);
		var indexName = $"bench-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";

		for (var i = 0; i < options.WarmupPasses + options.MeasuredPasses; i++)
		{
			await InitializeIndex(client, indexName, docs.First().Embedding.Length);

			sw.Restart();
			client
				.BulkAll(docs, x => x
					.Index(indexName)
					.MaxDegreeOfParallelism(options.MaxDegreeOfParallelism)
					.Size(options.ChunkSize)
				)
				.Wait(TimeSpan.FromHours(1), _ => {});
			sw.Stop();

			if (i >= options.WarmupPasses)
			{
				elapsedSeconds += sw.Elapsed.TotalSeconds;
			}

			await client.Indices.RefreshAsync(x => x.Indices(indexName)).ConfigureAwait(false);

			var count = await client.CountAsync(x => x.Indices(indexName)).ConfigureAwait(false);
			if (count.Count != docs.Length)
			{
				throw new Exception($"Document count mismatch: expected {docs.Length}, got {count.Count}");
			}
		}

		await client.Indices.DeleteAsync(indexName).ConfigureAwait(false);

		elapsedSeconds /= options.MeasuredPasses;
		var docsPerSec = docs.Length / elapsedSeconds;

		Console.WriteLine($"{((int)(elapsedSeconds * 1000)),4}ms / {((int)docsPerSec),4} docs/s");
	}

	private static OpenAiBenchmarkDocument[] LoadDataset(string filename, int repetitions)
	{
		return Enumerable.Repeat(0, repetitions).Select((_, i) => Load(filename, i)).Aggregate((a, b) => a.Union(b)).ToArray();

		static IEnumerable<OpenAiBenchmarkDocument> Load(string filename, int i)
		{
			using var fs = File.OpenRead(filename);
			return JsonSerializer
				.DeserializeAsyncEnumerable<OpenAiBenchmarkDocument>(fs, BenchmarkJsonSerializerContext.Default.OpenAiBenchmarkDocument, true)
				.ToBlockingEnumerable()
				.Select(x => x! with { Id = $"{i}_{x.Id}"})
				.ToArray();
		}
	}

	private static ElasticsearchClient CreateClient(VectorIngestOptions options)
	{
		var pool = new SingleNodePool(options.ElasticsearchEndpoint);

		var settings = new ElasticsearchClientSettings(pool,
				sourceSerializer: (_, settings) =>
					new DefaultSourceSerializer(settings, BenchmarkJsonSerializerContext.Default/*, x => x.Converters.Remove(x.Converters.OfType<JsonConverter<float>>().Single())*/)
			)
			.Authentication(new BasicAuthentication(options.ElasticsearchUsername, options.ElasticsearchPassword))
			.MemoryStreamFactory(new RecyclableMemoryStreamFactory())
			.EnableHttpCompression(false)
			.FloatVectorDataEncoding(options.UseBase64VectorEncoding ? FloatVectorDataEncoding.Base64 : FloatVectorDataEncoding.Legacy);

		return new ElasticsearchClient(settings);
	}

	private static async Task InitializeIndex(ElasticsearchClient client, string indexName, int vectorDimensions)
	{
		if (await client.Indices.ExistsAsync(indexName).ConfigureAwait(false) is { Exists: true })
		{
			await client.Indices.DeleteAsync(indexName).ConfigureAwait(false);
		}

		await client.Indices
			.CreateAsync<OpenAiBenchmarkDocument>(x => x
				.Index(indexName)
				.Mappings(x => x
					.Properties(x => x
						.Keyword(x => x.Id)
						.DenseVector(x => x.Embedding, x => x
							.Dims(vectorDimensions)
							.ElementType(DenseVectorElementType.Float)
							.IndexOptions(x => x.
								Type(DenseVectorIndexOptionsType.Flat)
							)
						)
						.Text(x => x.Title)
						.Text(x => x.Text)
					)
				)
				.WaitForActiveShards(1)
			)
			.ConfigureAwait(false);

		await client.Indices.RefreshAsync(x => x.Indices(indexName)).ConfigureAwait(false);
	}
}

[JsonSerializable(typeof(OpenAiBenchmarkDocument))]
[JsonSerializable(typeof(OpenAiBenchmarkDocument[]))]
[JsonSerializable(typeof(object))]
public sealed partial class BenchmarkJsonSerializerContext :
	JsonSerializerContext
{

}

public sealed record OpenAiBenchmarkDocument
{
	[JsonPropertyName("docid")]
	public required string Id { get; init; }

	[JsonPropertyName("title")]
	public required string Title { get; init; }

	[JsonPropertyName("text")]
	public required string Text { get; init; }

	[JsonConverter(typeof(FloatVectorDataConverter))]
	[JsonPropertyName("emb")]
	public ReadOnlyMemory<float> Embedding { get; init; }
}

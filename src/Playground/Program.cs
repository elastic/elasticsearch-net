// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Stack.ArtifactsApi;
using Elastic.Transport;
using ProcNet.Std;

var datasetFile = Environment.GetEnvironmentVariable("BENCHMARK_DATASET_FILE") ?? 
				  "C:\\Users\\Florian\\Desktop\\bench\\open_ai_corpus-initial-indexing-1k.json";

if (string.IsNullOrEmpty(datasetFile))
{
	Console.WriteLine("BENCHMARK_DATASET_FILE environment variable is not set. Exiting.");
	return;
}

var elasticsearchUrl = Environment.GetEnvironmentVariable("ELASTICSEARCH_URL");
var elasticsearchUser = Environment.GetEnvironmentVariable("ELASTICSEARCH_USER");
var elasticsearchPass = Environment.GetEnvironmentVariable("ELASTICSEARCH_PASS");

Console.WriteLine("Do you want me to start an ephemeral Elasticsearch instance for the benchmark?");
Console.Write("[y|n]: ");
var answer = Console.ReadKey();
Console.WriteLine();

EphemeralCluster? cluster = null;

if (answer.KeyChar is 'y' or 'Y')
{
	Console.WriteLine("Initializing ephemeral Elasticsearch cluster. This might take a few minutes.");
	cluster = new EphemeralCluster(new EphemeralClusterConfiguration(
		ElasticVersion.From("9.3.0-SNAPSHOT"), ClusterFeatures.XPack | ClusterFeatures.Security)
	{
		StartingPortNumber = 9202,
		TrialMode = XPackTrialMode.Trial
	});

	Console.WriteLine("Starting ephemeral Elasticsearch cluster ...");
	cluster.Start(NoopConsoleLineWriter.Instance, TimeSpan.FromHours(1));

	elasticsearchUrl = $"http://localhost:{cluster.ClusterConfiguration.StartingPortNumber}";
	elasticsearchUser = ClusterAuthentication.Admin.Username;
	elasticsearchPass = ClusterAuthentication.Admin.Password;
}
else
{
	if (string.IsNullOrEmpty(elasticsearchUrl))
	{
		Console.WriteLine("ELASTICSEARCH_URL environment variable is not set. Exiting.");
		return;
	}

	if (string.IsNullOrEmpty(elasticsearchUser))
	{
		Console.WriteLine("ELASTICSEARCH_USER environment variable is not set. Exiting.");
		return;
	}

	if (string.IsNullOrEmpty(elasticsearchPass))
	{
		Console.WriteLine("ELASTICSEARCH_PASS environment variable is not set. Exiting.");
		return;
	}
}

Console.WriteLine($"Using existing Elasticsearch instance at '{elasticsearchUrl}'");

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
	e.Cancel = true;
	if (cts.IsCancellationRequested)
	{
		return;
	}

	cts.Cancel();
};

try
{
	var options = new VectorIngestOptions
	{
		DatasetFile = "C:\\Users\\Florian\\Desktop\\bench\\open_ai_corpus-initial-indexing-1k.json",
		Repetitions = 20,
		MaxDegreeOfParallelism = 1,
		WarmupPasses = 5,
		MeasuredPasses = 3,
		ElasticsearchEndpoint = new Uri(elasticsearchUrl),
		ElasticsearchUsername = elasticsearchUser,
		ElasticsearchPassword = elasticsearchPass
	};

	var results = await VectorIngestBenchmark.Benchmark(options, cts.Token).ConfigureAwait(false);
	Console.WriteLine(JsonSerializer.Serialize(results.ToArray(), BenchmarkJsonSerializerContext.Default.VectorIngestResultArray));
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
}
finally
{
	cluster?.Dispose();
}

cluster?.WaitForExit(TimeSpan.FromHours(1));

internal class NoopConsoleLineWriter :
	IConsoleLineHandler
{
	public static NoopConsoleLineWriter Instance { get; } = new NoopConsoleLineWriter();

	public void Handle(LineOut lineOut)
	{
	}

	public void Handle(Exception e)
	{
	}
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

public sealed record VectorIngestResult
{
	[JsonPropertyName("dataset_size")]
	public required int DatasetSize { get; init; }

	[JsonPropertyName("chunk_size")]
	public required int ChunkSize { get; init; }

	[JsonPropertyName("float32")]
	public required VectorIngestEncodingResult Float32 { get; init; }

	[JsonPropertyName("base64")]
	public required VectorIngestEncodingResult Base64 { get; init; }
}

public sealed record VectorIngestEncodingResult
{
	/// <summary>
	/// The duration in milliseconds.
	/// </summary>
	[JsonPropertyName("duration")]
	public required long Duration { get; init; }
}

public static class VectorIngestBenchmark
{
	public static async Task<IEnumerable<VectorIngestResult>> Benchmark(VectorIngestOptions options, CancellationToken cancellationToken = default)
	{
		var documents = LoadDataset(options.DatasetFile, options.Repetitions);

		var chunkSizes = new[]
		{
			100, 250 ,500, 1000
		};

		var result = new List<VectorIngestResult>();

		foreach (var chunkSize in chunkSizes)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				break;
			}

			Console.WriteLine($"== Chunk size {chunkSize,4} == ");

			Console.Write("Float32 : ");
			var elapsedSecondsFloat32 = await Ingest(options, documents, chunkSize, FloatVectorDataEncoding.Legacy, cancellationToken);
			Console.WriteLine($"{((int)(elapsedSecondsFloat32 * 1000)),4}ms / {((int)(documents.Length / elapsedSecondsFloat32)),4} docs/s");

			if (cancellationToken.IsCancellationRequested)
			{
				break;
			}

			Console.Write("Base64 : ");
			var elapsedSecondsBase64 = await Ingest(options, documents, chunkSize, FloatVectorDataEncoding.Base64, cancellationToken);
			Console.WriteLine($"{((int)(elapsedSecondsBase64 * 1000)),4}ms / {((int)(documents.Length / elapsedSecondsBase64)),4} docs/s");

			Console.WriteLine($"Speedup: {(elapsedSecondsFloat32 / elapsedSecondsBase64):0.00}x");

			result.Add(new VectorIngestResult
			{
				DatasetSize = documents.Length,
				ChunkSize = chunkSize,
				Float32 = new VectorIngestEncodingResult
				{
					Duration = (int)(elapsedSecondsFloat32 * 1000)
				},
				Base64 = new VectorIngestEncodingResult
				{
					Duration = (int)(elapsedSecondsBase64 * 1000)
				}
			});
		}

		return result;
	}

	private static async Task<double> Ingest(
		VectorIngestOptions options,
		OpenAiBenchmarkDocument[] documents,
		int chunkSize,
		FloatVectorDataEncoding encoding,
		CancellationToken cancellationToken = default)
	{
		var sw = new Stopwatch();
		var elapsedSeconds = 0.0d;

		var client = CreateClient(options, encoding);
		var indexName = $"bench-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";

		for (var i = 0; i < options.WarmupPasses + options.MeasuredPasses; i++)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				break;
			}

			await InitializeIndex(client, indexName, documents.First().Embedding.Length, cancellationToken);

			sw.Restart();
			client
				.BulkAll(documents, x => x
					.Index(indexName)
					.MaxDegreeOfParallelism(options.MaxDegreeOfParallelism)
					.Size(chunkSize)
				)
				.Wait(TimeSpan.FromHours(1), _ => {});
			sw.Stop();

			if (i >= options.WarmupPasses)
			{
				elapsedSeconds += sw.Elapsed.TotalSeconds;
			}

			await client.Indices.RefreshAsync(x => x.Indices(indexName), cancellationToken).ConfigureAwait(false);

			var count = await client.CountAsync(x => x.Indices(indexName), cancellationToken).ConfigureAwait(false);
			if (!cancellationToken.IsCancellationRequested && (count.Count != documents.Length))
			{
				throw new Exception($"Document count mismatch: expected {documents.Length}, got {count.Count}");
			}
		}

		// ReSharper disable once MethodSupportsCancellation
		await client.Indices.DeleteAsync(indexName).ConfigureAwait(false);

		return elapsedSeconds / options.MeasuredPasses;
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

	private static ElasticsearchClient CreateClient(VectorIngestOptions options, FloatVectorDataEncoding encoding)
	{
		var pool = new SingleNodePool(options.ElasticsearchEndpoint);

		var settings = new ElasticsearchClientSettings(pool,
				sourceSerializer: (_, settings) =>
					new DefaultSourceSerializer(settings, BenchmarkJsonSerializerContext.Default/*, x => x.Converters.Remove(x.Converters.OfType<JsonConverter<float>>().Single())*/)
			)
			.Authentication(new BasicAuthentication(options.ElasticsearchUsername, options.ElasticsearchPassword))
			.MemoryStreamFactory(new RecyclableMemoryStreamFactory())
			.EnableHttpCompression(false)
			.FloatVectorDataEncoding(encoding);

		return new ElasticsearchClient(settings);
	}

	private static async Task InitializeIndex(ElasticsearchClient client, string indexName, int vectorDimensions, CancellationToken cancellationToken = default)
	{
		if (await client.Indices.ExistsAsync(indexName, cancellationToken).ConfigureAwait(false) is { Exists: true })
		{
			await client.Indices.DeleteAsync(indexName, cancellationToken).ConfigureAwait(false);
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
				.WaitForActiveShards(1),
				cancellationToken
			)
			.ConfigureAwait(false);

		await client.Indices.RefreshAsync(x => x.Indices(indexName), cancellationToken).ConfigureAwait(false);
	}
}

[JsonSerializable(typeof(OpenAiBenchmarkDocument))]
[JsonSerializable(typeof(OpenAiBenchmarkDocument[]))]
[JsonSerializable(typeof(VectorIngestResult[]))]
[JsonSourceGenerationOptions(WriteIndented = true)]
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

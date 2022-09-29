// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	internal sealed class DatafeedConverter : JsonConverter<Datafeed>
	{
		public override Datafeed Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? aggregations = default;
			Elastic.Clients.Elasticsearch.Ml.DatafeedAuthorization? authorization = default;
			Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? chunkingConfig = default;
			string datafeedId = default;
			Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig delayedDataCheckConfig = default;
			Elastic.Clients.Elasticsearch.Duration? frequency = default;
			IReadOnlyCollection<string>? indexes = default;
			IReadOnlyCollection<string> indices = default;
			Elastic.Clients.Elasticsearch.IndicesOptions? indicesOptions = default;
			string jobId = default;
			int? maxEmptySearches = default;
			Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer query = default;
			Elastic.Clients.Elasticsearch.Duration? queryDelay = default;
			Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? runtimeMappings = default;
			Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? scriptFields = default;
			int? scrollSize = default;
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "aggregations")
					{
						aggregations = JsonSerializer.Deserialize<Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>?>(ref reader, options);
						continue;
					}

					if (property == "authorization")
					{
						authorization = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DatafeedAuthorization?>(ref reader, options);
						continue;
					}

					if (property == "chunking_config")
					{
						chunkingConfig = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.ChunkingConfig?>(ref reader, options);
						continue;
					}

					if (property == "datafeed_id")
					{
						datafeedId = JsonSerializer.Deserialize<string>(ref reader, options);
						continue;
					}

					if (property == "delayed_data_check_config")
					{
						delayedDataCheckConfig = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig>(ref reader, options);
						continue;
					}

					if (property == "frequency")
					{
						frequency = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Duration?>(ref reader, options);
						continue;
					}

					if (property == "indexes")
					{
						indexes = JsonSerializer.Deserialize<IReadOnlyCollection<string>?>(ref reader, options);
						continue;
					}

					if (property == "indices")
					{
						indices = JsonSerializer.Deserialize<IReadOnlyCollection<string>>(ref reader, options);
						continue;
					}

					if (property == "indices_options")
					{
						indicesOptions = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.IndicesOptions?>(ref reader, options);
						continue;
					}

					if (property == "job_id")
					{
						jobId = JsonSerializer.Deserialize<string>(ref reader, options);
						continue;
					}

					if (property == "max_empty_searches")
					{
						maxEmptySearches = JsonSerializer.Deserialize<int?>(ref reader, options);
						continue;
					}

					if (property == "query")
					{
						query = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer>(ref reader, options);
						continue;
					}

					if (property == "query_delay")
					{
						queryDelay = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Duration?>(ref reader, options);
						continue;
					}

					if (property == "runtime_mappings")
					{
						runtimeMappings = JsonSerializer.Deserialize<Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>?>(ref reader, options);
						continue;
					}

					if (property == "script_fields")
					{
						scriptFields = JsonSerializer.Deserialize<Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>?>(ref reader, options);
						continue;
					}

					if (property == "scroll_size")
					{
						scrollSize = JsonSerializer.Deserialize<int?>(ref reader, options);
						continue;
					}
				}
			}

			return new Datafeed { Aggregations = aggregations, Authorization = authorization, ChunkingConfig = chunkingConfig, DatafeedId = datafeedId, DelayedDataCheckConfig = delayedDataCheckConfig, Frequency = frequency, Indexes = indexes, Indices = indices, IndicesOptions = indicesOptions, JobId = jobId, MaxEmptySearches = maxEmptySearches, Query = query, QueryDelay = queryDelay, RuntimeMappings = runtimeMappings, ScriptFields = scriptFields, ScrollSize = scrollSize };
		}

		public override void Write(Utf8JsonWriter writer, Datafeed value, JsonSerializerOptions options)
		{
			throw new NotImplementedException("'Datafeed' is a readonly type, used only on responses and does not support being written to JSON.");
		}
	}

	[JsonConverter(typeof(DatafeedConverter))]
	public sealed partial class Datafeed
	{
		public Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? Aggregations { get; init; }

		public Elastic.Clients.Elasticsearch.Ml.DatafeedAuthorization? Authorization { get; init; }

		public Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfig { get; init; }

		public string DatafeedId { get; init; }

		public Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig DelayedDataCheckConfig { get; init; }

		public Elastic.Clients.Elasticsearch.Duration? Frequency { get; init; }

		public IReadOnlyCollection<string>? Indexes { get; init; }

		public IReadOnlyCollection<string> Indices { get; init; }

		public Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptions { get; init; }

		public string JobId { get; init; }

		public int? MaxEmptySearches { get; init; }

		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer Query { get; init; }

		public Elastic.Clients.Elasticsearch.Duration? QueryDelay { get; init; }

		public Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappings { get; init; }

		public Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFields { get; init; }

		public int? ScrollSize { get; init; }
	}
}
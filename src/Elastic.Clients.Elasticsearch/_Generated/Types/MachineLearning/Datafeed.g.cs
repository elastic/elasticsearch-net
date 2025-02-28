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

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class DatafeedConverter : System.Text.Json.Serialization.JsonConverter<Datafeed>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregations = System.Text.Json.JsonEncodedText.Encode("aggregations");
	private static readonly System.Text.Json.JsonEncodedText PropAggregations1 = System.Text.Json.JsonEncodedText.Encode("aggs");
	private static readonly System.Text.Json.JsonEncodedText PropAuthorization = System.Text.Json.JsonEncodedText.Encode("authorization");
	private static readonly System.Text.Json.JsonEncodedText PropChunkingConfig = System.Text.Json.JsonEncodedText.Encode("chunking_config");
	private static readonly System.Text.Json.JsonEncodedText PropDatafeedId = System.Text.Json.JsonEncodedText.Encode("datafeed_id");
	private static readonly System.Text.Json.JsonEncodedText PropDelayedDataCheckConfig = System.Text.Json.JsonEncodedText.Encode("delayed_data_check_config");
	private static readonly System.Text.Json.JsonEncodedText PropFrequency = System.Text.Json.JsonEncodedText.Encode("frequency");
	private static readonly System.Text.Json.JsonEncodedText PropIndexes = System.Text.Json.JsonEncodedText.Encode("indexes");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropIndicesOptions = System.Text.Json.JsonEncodedText.Encode("indices_options");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropMaxEmptySearches = System.Text.Json.JsonEncodedText.Encode("max_empty_searches");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropQueryDelay = System.Text.Json.JsonEncodedText.Encode("query_delay");
	private static readonly System.Text.Json.JsonEncodedText PropRuntimeMappings = System.Text.Json.JsonEncodedText.Encode("runtime_mappings");
	private static readonly System.Text.Json.JsonEncodedText PropScriptFields = System.Text.Json.JsonEncodedText.Encode("script_fields");
	private static readonly System.Text.Json.JsonEncodedText PropScrollSize = System.Text.Json.JsonEncodedText.Encode("scroll_size");

	public override Datafeed Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>?> propAggregations = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorization?> propAuthorization = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig?> propChunkingConfig = default;
		LocalJsonValue<string> propDatafeedId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig> propDelayedDataCheckConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propFrequency = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propIndexes = default;
		LocalJsonValue<IReadOnlyCollection<string>> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndicesOptions?> propIndicesOptions = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<int?> propMaxEmptySearches = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query> propQuery = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propQueryDelay = default;
		LocalJsonValue<IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>?> propRuntimeMappings = default;
		LocalJsonValue<IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>?> propScriptFields = default;
		LocalJsonValue<int?> propScrollSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregations.TryReadProperty(ref reader, options, PropAggregations, static IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>(o, null, null)) || propAggregations.TryReadProperty(ref reader, options, PropAggregations1, static IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>(o, null, null)))
			{
				continue;
			}

			if (propAuthorization.TryReadProperty(ref reader, options, PropAuthorization, null))
			{
				continue;
			}

			if (propChunkingConfig.TryReadProperty(ref reader, options, PropChunkingConfig, null))
			{
				continue;
			}

			if (propDatafeedId.TryReadProperty(ref reader, options, PropDatafeedId, null))
			{
				continue;
			}

			if (propDelayedDataCheckConfig.TryReadProperty(ref reader, options, PropDelayedDataCheckConfig, null))
			{
				continue;
			}

			if (propFrequency.TryReadProperty(ref reader, options, PropFrequency, null))
			{
				continue;
			}

			if (propIndexes.TryReadProperty(ref reader, options, PropIndexes, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propIndicesOptions.TryReadProperty(ref reader, options, PropIndicesOptions, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propMaxEmptySearches.TryReadProperty(ref reader, options, PropMaxEmptySearches, null))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propQueryDelay.TryReadProperty(ref reader, options, PropQueryDelay, null))
			{
				continue;
			}

			if (propRuntimeMappings.TryReadProperty(ref reader, options, PropRuntimeMappings, static IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>(o, null, null)))
			{
				continue;
			}

			if (propScriptFields.TryReadProperty(ref reader, options, PropScriptFields, static IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.ScriptField>(o, null, null)))
			{
				continue;
			}

			if (propScrollSize.TryReadProperty(ref reader, options, PropScrollSize, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Datafeed
		{
			Aggregations = propAggregations.Value
,
			Authorization = propAuthorization.Value
,
			ChunkingConfig = propChunkingConfig.Value
,
			DatafeedId = propDatafeedId.Value
,
			DelayedDataCheckConfig = propDelayedDataCheckConfig.Value
,
			Frequency = propFrequency.Value
,
			Indexes = propIndexes.Value
,
			Indices = propIndices.Value
,
			IndicesOptions = propIndicesOptions.Value
,
			JobId = propJobId.Value
,
			MaxEmptySearches = propMaxEmptySearches.Value
,
			Query = propQuery.Value
,
			QueryDelay = propQueryDelay.Value
,
			RuntimeMappings = propRuntimeMappings.Value
,
			ScriptFields = propScriptFields.Value
,
			ScrollSize = propScrollSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Datafeed value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregations, value.Aggregations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>(o, v, null, null));
		writer.WriteProperty(options, PropAuthorization, value.Authorization, null, null);
		writer.WriteProperty(options, PropChunkingConfig, value.ChunkingConfig, null, null);
		writer.WriteProperty(options, PropDatafeedId, value.DatafeedId, null, null);
		writer.WriteProperty(options, PropDelayedDataCheckConfig, value.DelayedDataCheckConfig, null, null);
		writer.WriteProperty(options, PropFrequency, value.Frequency, null, null);
		writer.WriteProperty(options, PropIndexes, value.Indexes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropIndicesOptions, value.IndicesOptions, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropMaxEmptySearches, value.MaxEmptySearches, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropQueryDelay, value.QueryDelay, null, null);
		writer.WriteProperty(options, PropRuntimeMappings, value.RuntimeMappings, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>(o, v, null, null));
		writer.WriteProperty(options, PropScriptFields, value.ScriptFields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.ScriptField>(o, v, null, null));
		writer.WriteProperty(options, PropScrollSize, value.ScrollSize, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(DatafeedConverter))]
public sealed partial class Datafeed
{
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? Aggregations { get; init; }

	/// <summary>
	/// <para>
	/// The security privileges that the datafeed uses to run its queries. If Elastic Stack security features were disabled at the time of the most recent update to the datafeed, this property is omitted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DatafeedAuthorization? Authorization { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig? ChunkingConfig { get; init; }
	public string DatafeedId { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.DelayedDataCheckConfig DelayedDataCheckConfig { get; init; }
	public Elastic.Clients.Elasticsearch.Duration? Frequency { get; init; }
	public IReadOnlyCollection<string>? Indexes { get; init; }
	public IReadOnlyCollection<string> Indices { get; init; }
	public Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptions { get; init; }
	public string JobId { get; init; }
	public int? MaxEmptySearches { get; init; }
	public Elastic.Clients.Elasticsearch.QueryDsl.Query Query { get; init; }
	public Elastic.Clients.Elasticsearch.Duration? QueryDelay { get; init; }
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappings { get; init; }
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFields { get; init; }
	public int? ScrollSize { get; init; }
}
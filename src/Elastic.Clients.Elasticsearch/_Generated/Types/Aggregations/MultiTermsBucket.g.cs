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

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class MultiTermsBucketConverter : System.Text.Json.Serialization.JsonConverter<MultiTermsBucket>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocCount = System.Text.Json.JsonEncodedText.Encode("doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropDocCountErrorUpperBound = System.Text.Json.JsonEncodedText.Encode("doc_count_error_upper_bound");
	private static readonly System.Text.Json.JsonEncodedText PropKey = System.Text.Json.JsonEncodedText.Encode("key");
	private static readonly System.Text.Json.JsonEncodedText PropKeyAsString = System.Text.Json.JsonEncodedText.Encode("key_as_string");

	public override MultiTermsBucket Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.IAggregate> propAggregations = default;
		LocalJsonValue<long> propDocCount = default;
		LocalJsonValue<long?> propDocCountErrorUpperBound = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.FieldValue>> propKey = default;
		LocalJsonValue<string?> propKeyAsString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocCount.TryRead(ref reader, options, PropDocCount))
			{
				continue;
			}

			if (propDocCountErrorUpperBound.TryRead(ref reader, options, PropDocCountErrorUpperBound))
			{
				continue;
			}

			if (propKey.TryRead(ref reader, options, PropKey))
			{
				continue;
			}

			if (propKeyAsString.TryRead(ref reader, options, PropKeyAsString))
			{
				continue;
			}

			propAggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.IAggregate>();
			Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionaryConverter.ReadItem(ref reader, options, out string key, out Elastic.Clients.Elasticsearch.Aggregations.IAggregate value);
			propAggregations[key] = value;
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new MultiTermsBucket
		{
			Aggregations = new Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary(propAggregations)
,
			DocCount = propDocCount.Value
,
			DocCountErrorUpperBound = propDocCountErrorUpperBound.Value
,
			Key = propKey.Value
,
			KeyAsString = propKeyAsString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, MultiTermsBucket value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocCount, value.DocCount);
		writer.WriteProperty(options, PropDocCountErrorUpperBound, value.DocCountErrorUpperBound);
		writer.WriteProperty(options, PropKey, value.Key);
		writer.WriteProperty(options, PropKeyAsString, value.KeyAsString);
		if (value.Aggregations is not null)
		{
			foreach (var item in value.Aggregations)
			{
				Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionaryConverter.WriteItem(writer, options, item.Key, item.Value);
			}
		}

		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(MultiTermsBucketConverter))]
public sealed partial class MultiTermsBucket
{
	/// <summary>
	/// <para>
	/// Nested aggregations
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary Aggregations { get; init; }
	public long DocCount { get; init; }
	public long? DocCountErrorUpperBound { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.FieldValue> Key { get; init; }
	public string? KeyAsString { get; init; }
}
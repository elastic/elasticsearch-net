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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class IpRangeBucketConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.IpRangeBucket>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocCount = System.Text.Json.JsonEncodedText.Encode("doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropFrom = System.Text.Json.JsonEncodedText.Encode("from");
	private static readonly System.Text.Json.JsonEncodedText PropKey = System.Text.Json.JsonEncodedText.Encode("key");
	private static readonly System.Text.Json.JsonEncodedText PropTo = System.Text.Json.JsonEncodedText.Encode("to");

	public override Elastic.Clients.Elasticsearch.Aggregations.IpRangeBucket Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.IAggregate>? propAggregations = default;
		LocalJsonValue<long> propDocCount = default;
		LocalJsonValue<string?> propFrom = default;
		LocalJsonValue<string?> propKey = default;
		LocalJsonValue<string?> propTo = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocCount.TryReadProperty(ref reader, options, PropDocCount, null))
			{
				continue;
			}

			if (propFrom.TryReadProperty(ref reader, options, PropFrom, null))
			{
				continue;
			}

			if (propKey.TryReadProperty(ref reader, options, PropKey, null))
			{
				continue;
			}

			if (propTo.TryReadProperty(ref reader, options, PropTo, null))
			{
				continue;
			}

			propAggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.IAggregate>();
			Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionaryConverter.ReadItem(ref reader, options, out string key, out Elastic.Clients.Elasticsearch.Aggregations.IAggregate value);
			propAggregations[key] = value;
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.IpRangeBucket(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aggregations = new Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary(propAggregations),
			DocCount = propDocCount.Value,
			From = propFrom.Value,
			Key = propKey.Value,
			To = propTo.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.IpRangeBucket value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocCount, value.DocCount, null, null);
		writer.WriteProperty(options, PropFrom, value.From, null, null);
		writer.WriteProperty(options, PropKey, value.Key, null, null);
		writer.WriteProperty(options, PropTo, value.To, null, null);
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

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.IpRangeBucketConverter))]
public sealed partial class IpRangeBucket
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IpRangeBucket(long docCount)
	{
		DocCount = docCount;
	}
#if NET7_0_OR_GREATER
	public IpRangeBucket()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public IpRangeBucket()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IpRangeBucket(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Nested aggregations
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary? Aggregations { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long DocCount { get; set; }
	public string? From { get; set; }
	public string? Key { get; set; }
	public string? To { get; set; }
}
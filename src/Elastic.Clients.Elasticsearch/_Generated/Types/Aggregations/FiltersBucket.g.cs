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

internal sealed partial class FiltersBucketConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.FiltersBucket>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocCount = System.Text.Json.JsonEncodedText.Encode("doc_count");

	public override Elastic.Clients.Elasticsearch.Aggregations.FiltersBucket Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.IAggregate>? propAggregations = default;
		LocalJsonValue<long> propDocCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocCount.TryReadProperty(ref reader, options, PropDocCount, null))
			{
				continue;
			}

			propAggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.IAggregate>();
			Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionaryConverter.ReadItem(ref reader, options, out string key, out Elastic.Clients.Elasticsearch.Aggregations.IAggregate value);
			propAggregations[key] = value;
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.FiltersBucket(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aggregations = new Elastic.Clients.Elasticsearch.Aggregations.AggregateDictionary(propAggregations),
			DocCount = propDocCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.FiltersBucket value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocCount, value.DocCount, null, null);
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

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.FiltersBucketConverter))]
public sealed partial class FiltersBucket
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FiltersBucket(long docCount)
	{
		DocCount = docCount;
	}
#if NET7_0_OR_GREATER
	public FiltersBucket()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FiltersBucket()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FiltersBucket(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
}
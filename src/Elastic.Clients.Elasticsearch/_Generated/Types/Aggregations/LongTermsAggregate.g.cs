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

internal sealed partial class LongTermsAggregateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate>
{
	private static readonly System.Text.Json.JsonEncodedText PropBuckets = System.Text.Json.JsonEncodedText.Encode("buckets");
	private static readonly System.Text.Json.JsonEncodedText PropDocCountErrorUpperBound = System.Text.Json.JsonEncodedText.Encode("doc_count_error_upper_bound");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropSumOtherDocCount = System.Text.Json.JsonEncodedText.Encode("sum_other_doc_count");

	public override Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket>> propBuckets = default;
		LocalJsonValue<long?> propDocCountErrorUpperBound = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<long?> propSumOtherDocCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBuckets.TryReadProperty(ref reader, options, PropBuckets, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket>(o, null)!))
			{
				continue;
			}

			if (propDocCountErrorUpperBound.TryReadProperty(ref reader, options, PropDocCountErrorUpperBound, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propSumOtherDocCount.TryReadProperty(ref reader, options, PropSumOtherDocCount, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Buckets = propBuckets.Value,
			DocCountErrorUpperBound = propDocCountErrorUpperBound.Value,
			Meta = propMeta.Value,
			SumOtherDocCount = propSumOtherDocCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBuckets, value.Buckets, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket>(o, v, null));
		writer.WriteProperty(options, PropDocCountErrorUpperBound, value.DocCountErrorUpperBound, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropSumOtherDocCount, value.SumOtherDocCount, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Result of a <c>terms</c> aggregation when the field is some kind of whole number like a integer, long, or a date.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.LongTermsAggregateConverter))]
public sealed partial class LongTermsAggregate : Elastic.Clients.Elasticsearch.Aggregations.IAggregate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LongTermsAggregate(System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket> buckets)
	{
		Buckets = buckets;
	}
#if NET7_0_OR_GREATER
	public LongTermsAggregate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public LongTermsAggregate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal LongTermsAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.LongTermsBucket> Buckets { get; set; }
	public long? DocCountErrorUpperBound { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }
	public long? SumOtherDocCount { get; set; }

	string Elastic.Clients.Elasticsearch.Aggregations.IAggregate.Type => "lterms";
}
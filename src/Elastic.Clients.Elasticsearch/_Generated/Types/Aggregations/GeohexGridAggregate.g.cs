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

internal sealed partial class GeohexGridAggregateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate>
{
	private static readonly System.Text.Json.JsonEncodedText PropBuckets = System.Text.Json.JsonEncodedText.Encode("buckets");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");

	public override Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket>> propBuckets = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBuckets.TryReadProperty(ref reader, options, PropBuckets, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket>(o, null)!))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Buckets = propBuckets.Value,
			Meta = propMeta.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBuckets, value.Buckets, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket>(o, v, null));
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.GeohexGridAggregateConverter))]
public sealed partial class GeohexGridAggregate : Elastic.Clients.Elasticsearch.Aggregations.IAggregate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeohexGridAggregate(System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket> buckets)
	{
		Buckets = buckets;
	}
#if NET7_0_OR_GREATER
	public GeohexGridAggregate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeohexGridAggregate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeohexGridAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Aggregations.GeohexGridBucket> Buckets { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }

	string Elastic.Clients.Elasticsearch.Aggregations.IAggregate.Type => "geohex_grid";
}
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

internal sealed partial class HdrPercentilesAggregateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate>
{
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropValues = System.Text.Json.JsonEncodedText.Encode("values");

	public override Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.Percentiles> propValues = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propValues.TryReadProperty(ref reader, options, PropValues, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Meta = propMeta.Value,
			Values = propValues.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropValues, value.Values, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.HdrPercentilesAggregateConverter))]
public sealed partial class HdrPercentilesAggregate : Elastic.Clients.Elasticsearch.Aggregations.IAggregate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HdrPercentilesAggregate(Elastic.Clients.Elasticsearch.Aggregations.Percentiles values)
	{
		Values = values;
	}
#if NET7_0_OR_GREATER
	public HdrPercentilesAggregate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public HdrPercentilesAggregate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HdrPercentilesAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }

	string Elastic.Clients.Elasticsearch.Aggregations.IAggregate.Type => "hdr_percentiles";

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Aggregations.Percentiles Values { get; set; }
}
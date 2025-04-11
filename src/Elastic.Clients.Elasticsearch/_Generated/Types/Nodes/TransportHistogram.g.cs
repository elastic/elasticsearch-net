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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class TransportHistogramConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.TransportHistogram>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropGeMillis = System.Text.Json.JsonEncodedText.Encode("ge_millis");
	private static readonly System.Text.Json.JsonEncodedText PropLtMillis = System.Text.Json.JsonEncodedText.Encode("lt_millis");

	public override Elastic.Clients.Elasticsearch.Nodes.TransportHistogram Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propCount = default;
		LocalJsonValue<long?> propGeMillis = default;
		LocalJsonValue<long?> propLtMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propGeMillis.TryReadProperty(ref reader, options, PropGeMillis, null))
			{
				continue;
			}

			if (propLtMillis.TryReadProperty(ref reader, options, PropLtMillis, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.TransportHistogram(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			GeMillis = propGeMillis.Value,
			LtMillis = propLtMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.TransportHistogram value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropGeMillis, value.GeMillis, null, null);
		writer.WriteProperty(options, PropLtMillis, value.LtMillis, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.TransportHistogramConverter))]
public sealed partial class TransportHistogram
{
#if NET7_0_OR_GREATER
	public TransportHistogram()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public TransportHistogram()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TransportHistogram(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of times a transport thread took a period of time within the bounds of this bucket to handle an inbound message.
	/// </para>
	/// </summary>
	public long? Count { get; set; }

	/// <summary>
	/// <para>
	/// The inclusive lower bound of the bucket in milliseconds. May be omitted on the first bucket if this bucket has no lower bound.
	/// </para>
	/// </summary>
	public long? GeMillis { get; set; }

	/// <summary>
	/// <para>
	/// The exclusive upper bound of the bucket in milliseconds.
	/// May be omitted on the last bucket if this bucket has no upper bound.
	/// </para>
	/// </summary>
	public long? LtMillis { get; set; }
}
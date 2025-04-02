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

internal sealed partial class HttpRouteResponsesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.HttpRouteResponses>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropHandlingTimeHistogram = System.Text.Json.JsonEncodedText.Encode("handling_time_histogram");
	private static readonly System.Text.Json.JsonEncodedText PropSizeHistogram = System.Text.Json.JsonEncodedText.Encode("size_histogram");
	private static readonly System.Text.Json.JsonEncodedText PropTotalSizeInBytes = System.Text.Json.JsonEncodedText.Encode("total_size_in_bytes");

	public override Elastic.Clients.Elasticsearch.Nodes.HttpRouteResponses Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram>> propHandlingTimeHistogram = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram>> propSizeHistogram = default;
		LocalJsonValue<long> propTotalSizeInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propHandlingTimeHistogram.TryReadProperty(ref reader, options, PropHandlingTimeHistogram, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram>(o, null)!))
			{
				continue;
			}

			if (propSizeHistogram.TryReadProperty(ref reader, options, PropSizeHistogram, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram>(o, null)!))
			{
				continue;
			}

			if (propTotalSizeInBytes.TryReadProperty(ref reader, options, PropTotalSizeInBytes, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.HttpRouteResponses(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			HandlingTimeHistogram = propHandlingTimeHistogram.Value,
			SizeHistogram = propSizeHistogram.Value,
			TotalSizeInBytes = propTotalSizeInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.HttpRouteResponses value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropHandlingTimeHistogram, value.HandlingTimeHistogram, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram>(o, v, null));
		writer.WriteProperty(options, PropSizeHistogram, value.SizeHistogram, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram>(o, v, null));
		writer.WriteProperty(options, PropTotalSizeInBytes, value.TotalSizeInBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.HttpRouteResponsesConverter))]
public sealed partial class HttpRouteResponses
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HttpRouteResponses(long count, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram> handlingTimeHistogram, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram> sizeHistogram, long totalSizeInBytes)
	{
		Count = count;
		HandlingTimeHistogram = handlingTimeHistogram;
		SizeHistogram = sizeHistogram;
		TotalSizeInBytes = totalSizeInBytes;
	}
#if NET7_0_OR_GREATER
	public HttpRouteResponses()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public HttpRouteResponses()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HttpRouteResponses(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long Count { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.TimeHttpHistogram> HandlingTimeHistogram { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.SizeHttpHistogram> SizeHistogram { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TotalSizeInBytes { get; set; }
}
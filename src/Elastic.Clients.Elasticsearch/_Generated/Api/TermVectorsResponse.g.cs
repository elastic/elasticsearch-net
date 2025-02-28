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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class TermVectorsResponseConverter : System.Text.Json.Serialization.JsonConverter<TermVectorsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropFound = System.Text.Json.JsonEncodedText.Encode("found");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropTermVectors = System.Text.Json.JsonEncodedText.Encode("term_vectors");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("_version");

	public override TermVectorsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propFound = default;
		LocalJsonValue<string?> propId = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>?> propTermVectors = default;
		LocalJsonValue<long> propTook = default;
		LocalJsonValue<long> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFound.TryReadProperty(ref reader, options, PropFound, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propTermVectors.TryReadProperty(ref reader, options, PropTermVectors, static IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>(o, null, null)))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new TermVectorsResponse
		{
			Found = propFound.Value
,
			Id = propId.Value
,
			Index = propIndex.Value
,
			TermVectors = propTermVectors.Value
,
			Took = propTook.Value
,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, TermVectorsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFound, value.Found, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropTermVectors, value.TermVectors, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>? v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>(o, v, null, null));
		writer.WriteProperty(options, PropTook, value.Took, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(TermVectorsResponseConverter))]
public sealed partial class TermVectorsResponse : ElasticsearchResponse
{
	public bool Found { get; init; }
	public string? Id { get; init; }
	public string Index { get; init; }
	public IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>? TermVectors { get; init; }
	public long Took { get; init; }
	public long Version { get; init; }
}
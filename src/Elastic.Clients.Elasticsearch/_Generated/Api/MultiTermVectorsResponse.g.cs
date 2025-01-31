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

internal sealed partial class MultiTermVectorsResponseConverter : System.Text.Json.Serialization.JsonConverter<MultiTermVectorsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocs = System.Text.Json.JsonEncodedText.Encode("docs");

	public override MultiTermVectorsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Mtermvectors.MultiTermVectorsResult>> propDocs = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocs.TryRead(ref reader, options, PropDocs))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new MultiTermVectorsResponse
		{
			Docs = propDocs.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, MultiTermVectorsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocs, value.Docs);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(MultiTermVectorsResponseConverter))]
public sealed partial class MultiTermVectorsResponse : ElasticsearchResponse
{
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Mtermvectors.MultiTermVectorsResult> Docs { get; init; }
}
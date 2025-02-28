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

internal sealed partial class ReindexRethrottleResponseConverter : System.Text.Json.Serialization.JsonConverter<ReindexRethrottleResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropNodes = System.Text.Json.JsonEncodedText.Encode("nodes");

	public override ReindexRethrottleResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexNode>> propNodes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNodes.TryReadProperty(ref reader, options, PropNodes, static IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexNode> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexNode>(o, null, null)!))
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
		return new ReindexRethrottleResponse
		{
			Nodes = propNodes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ReindexRethrottleResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNodes, value.Nodes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexNode> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexNode>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(ReindexRethrottleResponseConverter))]
public sealed partial class ReindexRethrottleResponse : ElasticsearchResponse
{
	public IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.ReindexRethrottle.ReindexNode> Nodes { get; init; }
}
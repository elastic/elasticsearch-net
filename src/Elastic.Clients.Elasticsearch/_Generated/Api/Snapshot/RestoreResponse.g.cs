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

namespace Elastic.Clients.Elasticsearch.Snapshot;

internal sealed partial class RestoreResponseConverter : System.Text.Json.Serialization.JsonConverter<RestoreResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAccepted = System.Text.Json.JsonEncodedText.Encode("accepted");
	private static readonly System.Text.Json.JsonEncodedText PropSnapshot = System.Text.Json.JsonEncodedText.Encode("snapshot");

	public override RestoreResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAccepted = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Snapshot.SnapshotRestore?> propSnapshot = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAccepted.TryRead(ref reader, options, PropAccepted))
			{
				continue;
			}

			if (propSnapshot.TryRead(ref reader, options, PropSnapshot))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new RestoreResponse
		{
			Accepted = propAccepted.Value
,
			Snapshot = propSnapshot.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RestoreResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAccepted, value.Accepted);
		writer.WriteProperty(options, PropSnapshot, value.Snapshot);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RestoreResponseConverter))]
public sealed partial class RestoreResponse : ElasticsearchResponse
{
	public bool? Accepted { get; init; }
	public Elastic.Clients.Elasticsearch.Snapshot.SnapshotRestore? Snapshot { get; init; }
}
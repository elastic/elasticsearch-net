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

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

internal sealed partial class FollowInfoResponseConverter : System.Text.Json.Serialization.JsonConverter<FollowInfoResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropFollowerIndices = System.Text.Json.JsonEncodedText.Encode("follower_indices");

	public override FollowInfoResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndex>> propFollowerIndices = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFollowerIndices.TryRead(ref reader, options, PropFollowerIndices))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new FollowInfoResponse
		{
			FollowerIndices = propFollowerIndices.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, FollowInfoResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFollowerIndices, value.FollowerIndices);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(FollowInfoResponseConverter))]
public sealed partial class FollowInfoResponse : ElasticsearchResponse
{
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndex> FollowerIndices { get; init; }
}
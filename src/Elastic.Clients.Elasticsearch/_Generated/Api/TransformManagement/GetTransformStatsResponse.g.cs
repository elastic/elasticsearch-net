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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

internal sealed partial class GetTransformStatsResponseConverter : System.Text.Json.Serialization.JsonConverter<GetTransformStatsResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropTransforms = System.Text.Json.JsonEncodedText.Encode("transforms");

	public override GetTransformStatsResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propCount = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.TransformManagement.TransformStats>> propTransforms = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryRead(ref reader, options, PropCount))
			{
				continue;
			}

			if (propTransforms.TryRead(ref reader, options, PropTransforms))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new GetTransformStatsResponse
		{
			Count = propCount.Value
,
			Transforms = propTransforms.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetTransformStatsResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count);
		writer.WriteProperty(options, PropTransforms, value.Transforms);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetTransformStatsResponseConverter))]
public sealed partial class GetTransformStatsResponse : ElasticsearchResponse
{
	public long Count { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.TransformManagement.TransformStats> Transforms { get; init; }
}
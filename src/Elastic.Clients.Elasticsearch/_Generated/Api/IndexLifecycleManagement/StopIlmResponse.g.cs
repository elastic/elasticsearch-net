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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

internal sealed partial class StopIlmResponseConverter : System.Text.Json.Serialization.JsonConverter<StopIlmResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAcknowledged = System.Text.Json.JsonEncodedText.Encode("acknowledged");

	public override StopIlmResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAcknowledged = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAcknowledged.TryRead(ref reader, options, PropAcknowledged))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new StopIlmResponse
		{
			Acknowledged = propAcknowledged.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, StopIlmResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAcknowledged, value.Acknowledged);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(StopIlmResponseConverter))]
public sealed partial class StopIlmResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// For a successful response, this value is always true. On failure, an exception is returned instead.
	/// </para>
	/// </summary>
	public bool Acknowledged { get; init; }
}
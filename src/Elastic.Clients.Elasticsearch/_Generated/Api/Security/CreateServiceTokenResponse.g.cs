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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class CreateServiceTokenResponseConverter : System.Text.Json.Serialization.JsonConverter<CreateServiceTokenResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCreated = System.Text.Json.JsonEncodedText.Encode("created");
	private static readonly System.Text.Json.JsonEncodedText PropToken = System.Text.Json.JsonEncodedText.Encode("token");

	public override CreateServiceTokenResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propCreated = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.ServiceToken> propToken = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCreated.TryRead(ref reader, options, PropCreated))
			{
				continue;
			}

			if (propToken.TryRead(ref reader, options, PropToken))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new CreateServiceTokenResponse
		{
			Created = propCreated.Value
,
			Token = propToken.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, CreateServiceTokenResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCreated, value.Created);
		writer.WriteProperty(options, PropToken, value.Token);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(CreateServiceTokenResponseConverter))]
public sealed partial class CreateServiceTokenResponse : ElasticsearchResponse
{
	public bool Created { get; init; }
	public Elastic.Clients.Elasticsearch.Security.ServiceToken Token { get; init; }
}
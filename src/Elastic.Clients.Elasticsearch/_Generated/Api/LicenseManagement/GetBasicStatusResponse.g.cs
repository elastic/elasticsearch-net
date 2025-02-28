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

namespace Elastic.Clients.Elasticsearch.LicenseManagement;

internal sealed partial class GetBasicStatusResponseConverter : System.Text.Json.Serialization.JsonConverter<GetBasicStatusResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropEligibleToStartBasic = System.Text.Json.JsonEncodedText.Encode("eligible_to_start_basic");

	public override GetBasicStatusResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propEligibleToStartBasic = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEligibleToStartBasic.TryReadProperty(ref reader, options, PropEligibleToStartBasic, null))
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
		return new GetBasicStatusResponse
		{
			EligibleToStartBasic = propEligibleToStartBasic.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetBasicStatusResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEligibleToStartBasic, value.EligibleToStartBasic, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetBasicStatusResponseConverter))]
public sealed partial class GetBasicStatusResponse : ElasticsearchResponse
{
	public bool EligibleToStartBasic { get; init; }
}
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

internal sealed partial class GetTrialStatusResponseConverter : System.Text.Json.Serialization.JsonConverter<GetTrialStatusResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropEligibleToStartTrial = System.Text.Json.JsonEncodedText.Encode("eligible_to_start_trial");

	public override GetTrialStatusResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propEligibleToStartTrial = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEligibleToStartTrial.TryReadProperty(ref reader, options, PropEligibleToStartTrial, null))
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
		return new GetTrialStatusResponse
		{
			EligibleToStartTrial = propEligibleToStartTrial.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetTrialStatusResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEligibleToStartTrial, value.EligibleToStartTrial, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetTrialStatusResponseConverter))]
public sealed partial class GetTrialStatusResponse : ElasticsearchResponse
{
	public bool EligibleToStartTrial { get; init; }
}
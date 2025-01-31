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

internal sealed partial class RemovePolicyResponseConverter : System.Text.Json.Serialization.JsonConverter<RemovePolicyResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropFailedIndexes = System.Text.Json.JsonEncodedText.Encode("failed_indexes");
	private static readonly System.Text.Json.JsonEncodedText PropHasFailures = System.Text.Json.JsonEncodedText.Encode("has_failures");

	public override RemovePolicyResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<string>> propFailedIndexes = default;
		LocalJsonValue<bool> propHasFailures = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFailedIndexes.TryRead(ref reader, options, PropFailedIndexes))
			{
				continue;
			}

			if (propHasFailures.TryRead(ref reader, options, PropHasFailures))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new RemovePolicyResponse
		{
			FailedIndexes = propFailedIndexes.Value
,
			HasFailures = propHasFailures.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RemovePolicyResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFailedIndexes, value.FailedIndexes);
		writer.WriteProperty(options, PropHasFailures, value.HasFailures);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RemovePolicyResponseConverter))]
public sealed partial class RemovePolicyResponse : ElasticsearchResponse
{
	public IReadOnlyCollection<string> FailedIndexes { get; init; }
	public bool HasFailures { get; init; }
}
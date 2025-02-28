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

namespace Elastic.Clients.Elasticsearch.Rollup;

internal sealed partial class DeleteJobResponseConverter : System.Text.Json.Serialization.JsonConverter<DeleteJobResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAcknowledged = System.Text.Json.JsonEncodedText.Encode("acknowledged");
	private static readonly System.Text.Json.JsonEncodedText PropTaskFailures = System.Text.Json.JsonEncodedText.Encode("task_failures");

	public override DeleteJobResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAcknowledged = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>?> propTaskFailures = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAcknowledged.TryReadProperty(ref reader, options, PropAcknowledged, null))
			{
				continue;
			}

			if (propTaskFailures.TryReadProperty(ref reader, options, PropTaskFailures, static IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.TaskFailure>(o, null)))
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
		return new DeleteJobResponse
		{
			Acknowledged = propAcknowledged.Value
,
			TaskFailures = propTaskFailures.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, DeleteJobResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAcknowledged, value.Acknowledged, null, null);
		writer.WriteProperty(options, PropTaskFailures, value.TaskFailures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.TaskFailure>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(DeleteJobResponseConverter))]
public sealed partial class DeleteJobResponse : ElasticsearchResponse
{
	public bool Acknowledged { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? TaskFailures { get; init; }
}
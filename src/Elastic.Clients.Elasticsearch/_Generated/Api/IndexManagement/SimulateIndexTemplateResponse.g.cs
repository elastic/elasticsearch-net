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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class SimulateIndexTemplateResponseConverter : System.Text.Json.Serialization.JsonConverter<SimulateIndexTemplateResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropOverlapping = System.Text.Json.JsonEncodedText.Encode("overlapping");
	private static readonly System.Text.Json.JsonEncodedText PropTemplate = System.Text.Json.JsonEncodedText.Encode("template");

	public override SimulateIndexTemplateResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.Overlapping>?> propOverlapping = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.Template> propTemplate = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propOverlapping.TryReadProperty(ref reader, options, PropOverlapping, static IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.Overlapping>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.Overlapping>(o, null)))
			{
				continue;
			}

			if (propTemplate.TryReadProperty(ref reader, options, PropTemplate, null))
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
		return new SimulateIndexTemplateResponse
		{
			Overlapping = propOverlapping.Value
,
			Template = propTemplate.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, SimulateIndexTemplateResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropOverlapping, value.Overlapping, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.Overlapping>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.Overlapping>(o, v, null));
		writer.WriteProperty(options, PropTemplate, value.Template, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(SimulateIndexTemplateResponseConverter))]
public sealed partial class SimulateIndexTemplateResponse : ElasticsearchResponse
{
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.Overlapping>? Overlapping { get; init; }
	public Elastic.Clients.Elasticsearch.IndexManagement.Template Template { get; init; }
}
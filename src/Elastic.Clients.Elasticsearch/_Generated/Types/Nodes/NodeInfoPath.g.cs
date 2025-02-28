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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class NodeInfoPathConverter : System.Text.Json.Serialization.JsonConverter<NodeInfoPath>
{
	private static readonly System.Text.Json.JsonEncodedText PropData = System.Text.Json.JsonEncodedText.Encode("data");
	private static readonly System.Text.Json.JsonEncodedText PropHome = System.Text.Json.JsonEncodedText.Encode("home");
	private static readonly System.Text.Json.JsonEncodedText PropLogs = System.Text.Json.JsonEncodedText.Encode("logs");
	private static readonly System.Text.Json.JsonEncodedText PropRepo = System.Text.Json.JsonEncodedText.Encode("repo");

	public override NodeInfoPath Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<string>?> propData = default;
		LocalJsonValue<string?> propHome = default;
		LocalJsonValue<string?> propLogs = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propRepo = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propData.TryReadProperty(ref reader, options, PropData, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propHome.TryReadProperty(ref reader, options, PropHome, null))
			{
				continue;
			}

			if (propLogs.TryReadProperty(ref reader, options, PropLogs, null))
			{
				continue;
			}

			if (propRepo.TryReadProperty(ref reader, options, PropRepo, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new NodeInfoPath
		{
			Data = propData.Value
,
			Home = propHome.Value
,
			Logs = propLogs.Value
,
			Repo = propRepo.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, NodeInfoPath value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropData, value.Data, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropHome, value.Home, null, null);
		writer.WriteProperty(options, PropLogs, value.Logs, null, null);
		writer.WriteProperty(options, PropRepo, value.Repo, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(NodeInfoPathConverter))]
public sealed partial class NodeInfoPath
{
	public IReadOnlyCollection<string>? Data { get; init; }
	public string? Home { get; init; }
	public string? Logs { get; init; }
	public IReadOnlyCollection<string>? Repo { get; init; }
}
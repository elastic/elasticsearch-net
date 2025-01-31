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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class InfoResponseConverter : System.Text.Json.Serialization.JsonConverter<InfoResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropClusterName = System.Text.Json.JsonEncodedText.Encode("cluster_name");
	private static readonly System.Text.Json.JsonEncodedText PropClusterUuid = System.Text.Json.JsonEncodedText.Encode("cluster_uuid");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropTagline = System.Text.Json.JsonEncodedText.Encode("tagline");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override InfoResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClusterName = default;
		LocalJsonValue<string> propClusterUuid = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<string> propTagline = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClusterName.TryRead(ref reader, options, PropClusterName))
			{
				continue;
			}

			if (propClusterUuid.TryRead(ref reader, options, PropClusterUuid))
			{
				continue;
			}

			if (propName.TryRead(ref reader, options, PropName))
			{
				continue;
			}

			if (propTagline.TryRead(ref reader, options, PropTagline))
			{
				continue;
			}

			if (propVersion.TryRead(ref reader, options, PropVersion))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new InfoResponse
		{
			ClusterName = propClusterName.Value
,
			ClusterUuid = propClusterUuid.Value
,
			Name = propName.Value
,
			Tagline = propTagline.Value
,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, InfoResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClusterName, value.ClusterName);
		writer.WriteProperty(options, PropClusterUuid, value.ClusterUuid);
		writer.WriteProperty(options, PropName, value.Name);
		writer.WriteProperty(options, PropTagline, value.Tagline);
		writer.WriteProperty(options, PropVersion, value.Version);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(InfoResponseConverter))]
public sealed partial class InfoResponse : ElasticsearchResponse
{
	public string ClusterName { get; init; }
	public string ClusterUuid { get; init; }
	public string Name { get; init; }
	public string Tagline { get; init; }
	public Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo Version { get; init; }
}
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

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class GetIpLocationDatabaseResponseConverter : System.Text.Json.Serialization.JsonConverter<GetIpLocationDatabaseResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropDatabases = System.Text.Json.JsonEncodedText.Encode("databases");

	public override GetIpLocationDatabaseResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Ingest.IpDatabaseConfigurationMetadata>> propDatabases = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDatabases.TryReadProperty(ref reader, options, PropDatabases, static IReadOnlyCollection<Elastic.Clients.Elasticsearch.Ingest.IpDatabaseConfigurationMetadata> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.IpDatabaseConfigurationMetadata>(o, null)!))
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
		return new GetIpLocationDatabaseResponse
		{
			Databases = propDatabases.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetIpLocationDatabaseResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDatabases, value.Databases, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<Elastic.Clients.Elasticsearch.Ingest.IpDatabaseConfigurationMetadata> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.IpDatabaseConfigurationMetadata>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetIpLocationDatabaseResponseConverter))]
public sealed partial class GetIpLocationDatabaseResponse : ElasticsearchResponse
{
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Ingest.IpDatabaseConfigurationMetadata> Databases { get; init; }
}
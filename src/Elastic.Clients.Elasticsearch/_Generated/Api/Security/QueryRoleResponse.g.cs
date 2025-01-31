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

internal sealed partial class QueryRoleResponseConverter : System.Text.Json.Serialization.JsonConverter<QueryRoleResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropRoles = System.Text.Json.JsonEncodedText.Encode("roles");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override QueryRoleResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.QueryRole>> propRoles = default;
		LocalJsonValue<int> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryRead(ref reader, options, PropCount))
			{
				continue;
			}

			if (propRoles.TryRead(ref reader, options, PropRoles))
			{
				continue;
			}

			if (propTotal.TryRead(ref reader, options, PropTotal))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new QueryRoleResponse
		{
			Count = propCount.Value
,
			Roles = propRoles.Value
,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, QueryRoleResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count);
		writer.WriteProperty(options, PropRoles, value.Roles);
		writer.WriteProperty(options, PropTotal, value.Total);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(QueryRoleResponseConverter))]
public sealed partial class QueryRoleResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// The number of roles returned in the response.
	/// </para>
	/// </summary>
	public int Count { get; init; }

	/// <summary>
	/// <para>
	/// The list of roles.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.QueryRole> Roles { get; init; }

	/// <summary>
	/// <para>
	/// The total number of roles found.
	/// </para>
	/// </summary>
	public int Total { get; init; }
}
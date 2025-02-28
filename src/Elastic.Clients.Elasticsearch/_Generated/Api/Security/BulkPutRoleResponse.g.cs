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

internal sealed partial class BulkPutRoleResponseConverter : System.Text.Json.Serialization.JsonConverter<BulkPutRoleResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCreated = System.Text.Json.JsonEncodedText.Encode("created");
	private static readonly System.Text.Json.JsonEncodedText PropErrors = System.Text.Json.JsonEncodedText.Encode("errors");
	private static readonly System.Text.Json.JsonEncodedText PropNoop = System.Text.Json.JsonEncodedText.Encode("noop");
	private static readonly System.Text.Json.JsonEncodedText PropUpdated = System.Text.Json.JsonEncodedText.Encode("updated");

	public override BulkPutRoleResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<IReadOnlyCollection<string>?> propCreated = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.BulkError?> propErrors = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propNoop = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propUpdated = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCreated.TryReadProperty(ref reader, options, PropCreated, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propErrors.TryReadProperty(ref reader, options, PropErrors, null))
			{
				continue;
			}

			if (propNoop.TryReadProperty(ref reader, options, PropNoop, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propUpdated.TryReadProperty(ref reader, options, PropUpdated, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new BulkPutRoleResponse
		{
			Created = propCreated.Value
,
			Errors = propErrors.Value
,
			Noop = propNoop.Value
,
			Updated = propUpdated.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, BulkPutRoleResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCreated, value.Created, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropErrors, value.Errors, null, null);
		writer.WriteProperty(options, PropNoop, value.Noop, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropUpdated, value.Updated, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(BulkPutRoleResponseConverter))]
public sealed partial class BulkPutRoleResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// Array of created roles
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? Created { get; init; }

	/// <summary>
	/// <para>
	/// Present if any updates resulted in errors
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.BulkError? Errors { get; init; }

	/// <summary>
	/// <para>
	/// Array of role names without any changes
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? Noop { get; init; }

	/// <summary>
	/// <para>
	/// Array of updated roles
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? Updated { get; init; }
}
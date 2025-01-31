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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class UserIndicesPrivilegesConverter : System.Text.Json.Serialization.JsonConverter<UserIndicesPrivileges>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowRestrictedIndices = System.Text.Json.JsonEncodedText.Encode("allow_restricted_indices");
	private static readonly System.Text.Json.JsonEncodedText PropFieldSecurity = System.Text.Json.JsonEncodedText.Encode("field_security");
	private static readonly System.Text.Json.JsonEncodedText PropNames = System.Text.Json.JsonEncodedText.Encode("names");
	private static readonly System.Text.Json.JsonEncodedText PropPrivileges = System.Text.Json.JsonEncodedText.Encode("privileges");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");

	public override UserIndicesPrivileges Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAllowRestrictedIndices = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.FieldSecurity>?> propFieldSecurity = default;
		LocalJsonValue<IReadOnlyCollection<string>> propNames = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege>> propPrivileges = default;
		LocalJsonValue<object?> propQuery = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowRestrictedIndices.TryRead(ref reader, options, PropAllowRestrictedIndices))
			{
				continue;
			}

			if (propFieldSecurity.TryRead(ref reader, options, PropFieldSecurity))
			{
				continue;
			}

			if (propNames.TryRead(ref reader, options, PropNames, typeof(SingleOrManyMarker<IReadOnlyCollection<string>, string>)))
			{
				continue;
			}

			if (propPrivileges.TryRead(ref reader, options, PropPrivileges))
			{
				continue;
			}

			if (propQuery.TryRead(ref reader, options, PropQuery))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new UserIndicesPrivileges
		{
			AllowRestrictedIndices = propAllowRestrictedIndices.Value
,
			FieldSecurity = propFieldSecurity.Value
,
			Names = propNames.Value
,
			Privileges = propPrivileges.Value
,
			Query = propQuery.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, UserIndicesPrivileges value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowRestrictedIndices, value.AllowRestrictedIndices);
		writer.WriteProperty(options, PropFieldSecurity, value.FieldSecurity);
		writer.WriteProperty(options, PropNames, value.Names, null, typeof(SingleOrManyMarker<IReadOnlyCollection<string>, string>));
		writer.WriteProperty(options, PropPrivileges, value.Privileges);
		writer.WriteProperty(options, PropQuery, value.Query);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(UserIndicesPrivilegesConverter))]
public sealed partial class UserIndicesPrivileges
{
	/// <summary>
	/// <para>
	/// Set to <c>true</c> if using wildcard or regular expressions for patterns that cover restricted indices. Implicitly, restricted indices have limited privileges that can cause pattern tests to fail. If restricted indices are explicitly included in the <c>names</c> list, Elasticsearch checks privileges against these indices regardless of the value set for <c>allow_restricted_indices</c>.
	/// </para>
	/// </summary>
	public bool AllowRestrictedIndices { get; init; }

	/// <summary>
	/// <para>
	/// The document fields that the owners of the role have read access to.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.FieldSecurity>? FieldSecurity { get; init; }

	/// <summary>
	/// <para>
	/// A list of indices (or index name patterns) to which the permissions in this entry apply.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string> Names { get; init; }

	/// <summary>
	/// <para>
	/// The index level privileges that owners of the role have on the specified indices.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> Privileges { get; init; }

	/// <summary>
	/// <para>
	/// Search queries that define the documents the user has access to. A document within the specified indices must match these queries for it to be accessible by the owners of the role.
	/// </para>
	/// </summary>
	public object? Query { get; init; }
}
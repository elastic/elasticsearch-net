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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class RemoteUserIndicesPrivilegesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowRestrictedIndices = System.Text.Json.JsonEncodedText.Encode("allow_restricted_indices");
	private static readonly System.Text.Json.JsonEncodedText PropClusters = System.Text.Json.JsonEncodedText.Encode("clusters");
	private static readonly System.Text.Json.JsonEncodedText PropFieldSecurity = System.Text.Json.JsonEncodedText.Encode("field_security");
	private static readonly System.Text.Json.JsonEncodedText PropNames = System.Text.Json.JsonEncodedText.Encode("names");
	private static readonly System.Text.Json.JsonEncodedText PropPrivileges = System.Text.Json.JsonEncodedText.Encode("privileges");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");

	public override Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAllowRestrictedIndices = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propClusters = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.FieldSecurity>?> propFieldSecurity = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propNames = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege>> propPrivileges = default;
		LocalJsonValue<object?> propQuery = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowRestrictedIndices.TryReadProperty(ref reader, options, PropAllowRestrictedIndices, null))
			{
				continue;
			}

			if (propClusters.TryReadProperty(ref reader, options, PropClusters, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propFieldSecurity.TryReadProperty(ref reader, options, PropFieldSecurity, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.FieldSecurity>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.FieldSecurity>(o, null)))
			{
				continue;
			}

			if (propNames.TryReadProperty(ref reader, options, PropNames, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propPrivileges.TryReadProperty(ref reader, options, PropPrivileges, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.IndexPrivilege>(o, null)!))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowRestrictedIndices = propAllowRestrictedIndices.Value,
			Clusters = propClusters.Value,
			FieldSecurity = propFieldSecurity.Value,
			Names = propNames.Value,
			Privileges = propPrivileges.Value,
			Query = propQuery.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowRestrictedIndices, value.AllowRestrictedIndices, null, null);
		writer.WriteProperty(options, PropClusters, value.Clusters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropFieldSecurity, value.FieldSecurity, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.FieldSecurity>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.FieldSecurity>(o, v, null));
		writer.WriteProperty(options, PropNames, value.Names, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropPrivileges, value.Privileges, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.IndexPrivilege>(o, v, null));
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivilegesConverter))]
public sealed partial class RemoteUserIndicesPrivileges
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoteUserIndicesPrivileges(bool allowRestrictedIndices, System.Collections.Generic.IReadOnlyCollection<string> clusters, System.Collections.Generic.ICollection<string> names, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> privileges)
	{
		AllowRestrictedIndices = allowRestrictedIndices;
		Clusters = clusters;
		Names = names;
		Privileges = privileges;
	}
#if NET7_0_OR_GREATER
	public RemoteUserIndicesPrivileges()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RemoteUserIndicesPrivileges()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RemoteUserIndicesPrivileges(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Set to <c>true</c> if using wildcard or regular expressions for patterns that cover restricted indices. Implicitly, restricted indices have limited privileges that can cause pattern tests to fail. If restricted indices are explicitly included in the <c>names</c> list, Elasticsearch checks privileges against these indices regardless of the value set for <c>allow_restricted_indices</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool AllowRestrictedIndices { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> Clusters { get; set; }

	/// <summary>
	/// <para>
	/// The document fields that the owners of the role have read access to.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.FieldSecurity>? FieldSecurity { get; set; }

	/// <summary>
	/// <para>
	/// A list of indices (or index name patterns) to which the permissions in this entry apply.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> Names { get; set; }

	/// <summary>
	/// <para>
	/// The index level privileges that owners of the role have on the specified indices.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> Privileges { get; set; }

	/// <summary>
	/// <para>
	/// Search queries that define the documents the user has access to. A document within the specified indices must match these queries for it to be accessible by the owners of the role.
	/// </para>
	/// </summary>
	public object? Query { get; set; }
}
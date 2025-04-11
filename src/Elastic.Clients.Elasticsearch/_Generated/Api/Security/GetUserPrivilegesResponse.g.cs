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

internal sealed partial class GetUserPrivilegesResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropApplications = System.Text.Json.JsonEncodedText.Encode("applications");
	private static readonly System.Text.Json.JsonEncodedText PropCluster = System.Text.Json.JsonEncodedText.Encode("cluster");
	private static readonly System.Text.Json.JsonEncodedText PropGlobal = System.Text.Json.JsonEncodedText.Encode("global");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteCluster = System.Text.Json.JsonEncodedText.Encode("remote_cluster");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteIndices = System.Text.Json.JsonEncodedText.Encode("remote_indices");
	private static readonly System.Text.Json.JsonEncodedText PropRunAs = System.Text.Json.JsonEncodedText.Encode("run_as");

	public override Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>> propApplications = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propCluster = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege>> propGlobal = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges>> propIndices = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>?> propRemoteCluster = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>?> propRemoteIndices = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propRunAs = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propApplications.TryReadProperty(ref reader, options, PropApplications, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>(o, null)!))
			{
				continue;
			}

			if (propCluster.TryReadProperty(ref reader, options, PropCluster, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propGlobal.TryReadProperty(ref reader, options, PropGlobal, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege>(o, null)!))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges>(o, null)!))
			{
				continue;
			}

			if (propRemoteCluster.TryReadProperty(ref reader, options, PropRemoteCluster, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>(o, null)))
			{
				continue;
			}

			if (propRemoteIndices.TryReadProperty(ref reader, options, PropRemoteIndices, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>(o, null)))
			{
				continue;
			}

			if (propRunAs.TryReadProperty(ref reader, options, PropRunAs, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Applications = propApplications.Value,
			Cluster = propCluster.Value,
			Global = propGlobal.Value,
			Indices = propIndices.Value,
			RemoteCluster = propRemoteCluster.Value,
			RemoteIndices = propRemoteIndices.Value,
			RunAs = propRunAs.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropApplications, value.Applications, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>(o, v, null));
		writer.WriteProperty(options, PropCluster, value.Cluster, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropGlobal, value.Global, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege>(o, v, null));
		writer.WriteProperty(options, PropIndices, value.Indices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges>(o, v, null));
		writer.WriteProperty(options, PropRemoteCluster, value.RemoteCluster, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>(o, v, null));
		writer.WriteProperty(options, PropRemoteIndices, value.RemoteIndices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>(o, v, null));
		writer.WriteProperty(options, PropRunAs, value.RunAs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesResponseConverter))]
public sealed partial class GetUserPrivilegesResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetUserPrivilegesResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetUserPrivilegesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges> Applications { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<string> Cluster { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege> Global { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.UserIndicesPrivileges> Indices { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivileges>? RemoteCluster { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteUserIndicesPrivileges>? RemoteIndices { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyCollection<string> RunAs { get; set; }
}
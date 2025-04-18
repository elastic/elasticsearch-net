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

internal sealed partial class GetBuiltinPrivilegesResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCluster = System.Text.Json.JsonEncodedText.Encode("cluster");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropRemoteCluster = System.Text.Json.JsonEncodedText.Encode("remote_cluster");

	public override Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>> propCluster = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propIndex = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege>> propRemoteCluster = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCluster.TryReadProperty(ref reader, options, PropCluster, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>(o, null)!))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propRemoteCluster.TryReadProperty(ref reader, options, PropRemoteCluster, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Cluster = propCluster.Value,
			Index = propIndex.Value,
			RemoteCluster = propRemoteCluster.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCluster, value.Cluster, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege>(o, v, null));
		writer.WriteProperty(options, PropIndex, value.Index, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropRemoteCluster, value.RemoteCluster, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesResponseConverter))]
public sealed partial class GetBuiltinPrivilegesResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetBuiltinPrivilegesResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetBuiltinPrivilegesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The list of cluster privileges that are understood by this version of Elasticsearch.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ClusterPrivilege> Cluster { get; set; }

	/// <summary>
	/// <para>
	/// The list of index privileges that are understood by this version of Elasticsearch.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> Index { get; set; }

	/// <summary>
	/// <para>
	/// The list of remote_cluster privileges that are understood by this version of Elasticsearch.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.RemoteClusterPrivilege> RemoteCluster { get; set; }
}
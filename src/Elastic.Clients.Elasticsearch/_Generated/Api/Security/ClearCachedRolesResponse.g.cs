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

internal sealed partial class ClearCachedRolesResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.ClearCachedRolesResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropClusterName = System.Text.Json.JsonEncodedText.Encode("cluster_name");
	private static readonly System.Text.Json.JsonEncodedText PropNodes = System.Text.Json.JsonEncodedText.Encode("nodes");
	private static readonly System.Text.Json.JsonEncodedText PropNodeStats = System.Text.Json.JsonEncodedText.Encode("_nodes");

	public override Elastic.Clients.Elasticsearch.Security.ClearCachedRolesResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClusterName = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.ClusterNode>> propNodes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.NodeStatistics> propNodeStats = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClusterName.TryReadProperty(ref reader, options, PropClusterName, null))
			{
				continue;
			}

			if (propNodes.TryReadProperty(ref reader, options, PropNodes, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.ClusterNode> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Security.ClusterNode>(o, null, null)!))
			{
				continue;
			}

			if (propNodeStats.TryReadProperty(ref reader, options, PropNodeStats, null))
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
		return new Elastic.Clients.Elasticsearch.Security.ClearCachedRolesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ClusterName = propClusterName.Value,
			Nodes = propNodes.Value,
			NodeStats = propNodeStats.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.ClearCachedRolesResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClusterName, value.ClusterName, null, null);
		writer.WriteProperty(options, PropNodes, value.Nodes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.ClusterNode> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Security.ClusterNode>(o, v, null, null));
		writer.WriteProperty(options, PropNodeStats, value.NodeStats, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.ClearCachedRolesResponseConverter))]
public sealed partial class ClearCachedRolesResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearCachedRolesResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClearCachedRolesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		string ClusterName { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.ClusterNode> Nodes { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.NodeStatistics NodeStats { get; set; }
}
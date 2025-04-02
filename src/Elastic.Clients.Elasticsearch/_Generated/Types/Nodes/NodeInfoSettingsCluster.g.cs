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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class NodeInfoSettingsClusterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsCluster>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeprecationIndexing = System.Text.Json.JsonEncodedText.Encode("deprecation_indexing");
	private static readonly System.Text.Json.JsonEncodedText PropElection = System.Text.Json.JsonEncodedText.Encode("election");
	private static readonly System.Text.Json.JsonEncodedText PropInitialMasterNodes = System.Text.Json.JsonEncodedText.Encode("initial_master_nodes");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");

	public override Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsCluster Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.DeprecationIndexing?> propDeprecationIndexing = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsClusterElection> propElection = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propInitialMasterNodes = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexRouting?> propRouting = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDeprecationIndexing.TryReadProperty(ref reader, options, PropDeprecationIndexing, null))
			{
				continue;
			}

			if (propElection.TryReadProperty(ref reader, options, PropElection, null))
			{
				continue;
			}

			if (propInitialMasterNodes.TryReadProperty(ref reader, options, PropInitialMasterNodes, static System.Collections.Generic.IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsCluster(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DeprecationIndexing = propDeprecationIndexing.Value,
			Election = propElection.Value,
			InitialMasterNodes = propInitialMasterNodes.Value,
			Name = propName.Value,
			Routing = propRouting.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsCluster value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDeprecationIndexing, value.DeprecationIndexing, null, null);
		writer.WriteProperty(options, PropElection, value.Election, null, null);
		writer.WriteProperty(options, PropInitialMasterNodes, value.InitialMasterNodes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsClusterConverter))]
public sealed partial class NodeInfoSettingsCluster
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodeInfoSettingsCluster(Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsClusterElection election, string name)
	{
		Election = election;
		Name = name;
	}
#if NET7_0_OR_GREATER
	public NodeInfoSettingsCluster()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public NodeInfoSettingsCluster()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodeInfoSettingsCluster(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Nodes.DeprecationIndexing? DeprecationIndexing { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsClusterElection Election { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<string>? InitialMasterNodes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRouting? Routing { get; set; }
}
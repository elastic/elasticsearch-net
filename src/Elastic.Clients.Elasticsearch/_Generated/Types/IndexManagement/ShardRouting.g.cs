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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ShardRoutingConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting>
{
	private static readonly System.Text.Json.JsonEncodedText PropNode = System.Text.Json.JsonEncodedText.Encode("node");
	private static readonly System.Text.Json.JsonEncodedText PropPrimary = System.Text.Json.JsonEncodedText.Encode("primary");
	private static readonly System.Text.Json.JsonEncodedText PropRelocatingNode = System.Text.Json.JsonEncodedText.Encode("relocating_node");
	private static readonly System.Text.Json.JsonEncodedText PropState = System.Text.Json.JsonEncodedText.Encode("state");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propNode = default;
		LocalJsonValue<bool> propPrimary = default;
		LocalJsonValue<string?> propRelocatingNode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardRoutingState> propState = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNode.TryReadProperty(ref reader, options, PropNode, null))
			{
				continue;
			}

			if (propPrimary.TryReadProperty(ref reader, options, PropPrimary, null))
			{
				continue;
			}

			if (propRelocatingNode.TryReadProperty(ref reader, options, PropRelocatingNode, null))
			{
				continue;
			}

			if (propState.TryReadProperty(ref reader, options, PropState, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Node = propNode.Value,
			Primary = propPrimary.Value,
			RelocatingNode = propRelocatingNode.Value,
			State = propState.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardRouting value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNode, value.Node, null, null);
		writer.WriteProperty(options, PropPrimary, value.Primary, null, null);
		writer.WriteProperty(options, PropRelocatingNode, value.RelocatingNode, null, null);
		writer.WriteProperty(options, PropState, value.State, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardRoutingConverter))]
public sealed partial class ShardRouting
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardRouting(string node, bool primary, Elastic.Clients.Elasticsearch.IndexManagement.ShardRoutingState state)
	{
		Node = node;
		Primary = primary;
		State = state;
	}
#if NET7_0_OR_GREATER
	public ShardRouting()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardRouting()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardRouting(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Node { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Primary { get; set; }
	public string? RelocatingNode { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.ShardRoutingState State { get; set; }
}
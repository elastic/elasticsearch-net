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

internal sealed partial class ShardStoreConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardStore>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllocation = System.Text.Json.JsonEncodedText.Encode("allocation");
	private static readonly System.Text.Json.JsonEncodedText PropAllocationId = System.Text.Json.JsonEncodedText.Encode("allocation_id");
	private static readonly System.Text.Json.JsonEncodedText PropStoreException = System.Text.Json.JsonEncodedText.Encode("store_exception");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardStore Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation> propAllocation = default;
		LocalJsonValue<string?> propAllocationId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreNode> propNode = default;
		LocalJsonValue<string> propNodeId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreException?> propStoreException = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllocation.TryReadProperty(ref reader, options, PropAllocation, null))
			{
				continue;
			}

			if (propAllocationId.TryReadProperty(ref reader, options, PropAllocationId, null))
			{
				continue;
			}

			if (propStoreException.TryReadProperty(ref reader, options, PropStoreException, null))
			{
				continue;
			}

			propNodeId.Initialized = propNode.Initialized = true;
			reader.ReadProperty(options, out propNodeId.Value, out propNode.Value, null, null);
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Allocation = propAllocation.Value,
			AllocationId = propAllocationId.Value,
			Node = propNode.Value,
			NodeId = propNodeId.Value,
			StoreException = propStoreException.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardStore value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllocation, value.Allocation, null, null);
		writer.WriteProperty(options, PropAllocationId, value.AllocationId, null, null);
		writer.WriteProperty(options, PropStoreException, value.StoreException, null, null);
		writer.WriteProperty(options, value.NodeId, value.Node, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreConverter))]
public sealed partial class ShardStore
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardStore(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation allocation, Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreNode node, string nodeId)
	{
		Allocation = allocation;
		Node = node;
		NodeId = nodeId;
	}
#if NET7_0_OR_GREATER
	public ShardStore()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardStore()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation Allocation { get; set; }
	public string? AllocationId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreNode Node { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string NodeId { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreException? StoreException { get; set; }
}
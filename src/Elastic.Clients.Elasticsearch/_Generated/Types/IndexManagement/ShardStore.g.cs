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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ShardStoreConverter : JsonConverter<ShardStore>
{
	public override ShardStore Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation allocation = default;
		string? allocationId = default;
		Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreException? storeException = default;
		string nodeId = default;
		Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreNode node = default;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "allocation")
				{
					allocation = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation>(ref reader, options);
					continue;
				}

				if (property == "allocation_id")
				{
					allocationId = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "store_exception")
				{
					storeException = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreException?>(ref reader, options);
					continue;
				}

				nodeId = property;
				reader.Read();
				node = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreNode>(ref reader, options);
			}
		}

		return new ShardStore { Allocation = allocation, AllocationId = allocationId, Node = node, NodeId = nodeId, StoreException = storeException };
	}

	public override void Write(Utf8JsonWriter writer, ShardStore value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("'ShardStore' is a readonly type, used only on responses and does not support being written to JSON.");
	}
}

[JsonConverter(typeof(ShardStoreConverter))]
public sealed partial class ShardStore
{
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreAllocation Allocation { get; init; }
	public string? AllocationId { get; init; }
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreNode Node { get; init; }
	public string NodeId { get; init; }
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreException? StoreException { get; init; }
}
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

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Cluster.Reroute
{
	public partial class CommandAllocatePrimaryAction
	{
		[JsonInclude]
		[JsonPropertyName("index")]
		public Elastic.Clients.Elasticsearch.IndexName Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("shard")]
		public int Shard { get; set; }

		[JsonInclude]
		[JsonPropertyName("node")]
		public string Node { get; set; }

		[JsonInclude]
		[JsonPropertyName("accept_data_loss")]
		public bool AcceptDataLoss { get; set; }
	}

	public sealed partial class CommandAllocatePrimaryActionDescriptor : DescriptorBase<CommandAllocatePrimaryActionDescriptor>
	{
		public CommandAllocatePrimaryActionDescriptor()
		{
		}

		internal CommandAllocatePrimaryActionDescriptor(Action<CommandAllocatePrimaryActionDescriptor> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.IndexName IndexValue { get; private set; }

		internal int ShardValue { get; private set; }

		internal string NodeValue { get; private set; }

		internal bool AcceptDataLossValue { get; private set; }

		public CommandAllocatePrimaryActionDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index) => Assign(index, (a, v) => a.IndexValue = v);
		public CommandAllocatePrimaryActionDescriptor Shard(int shard) => Assign(shard, (a, v) => a.ShardValue = v);
		public CommandAllocatePrimaryActionDescriptor Node(string node) => Assign(node, (a, v) => a.NodeValue = v);
		public CommandAllocatePrimaryActionDescriptor AcceptDataLoss(bool acceptDataLoss = true) => Assign(acceptDataLoss, (a, v) => a.AcceptDataLossValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("index");
			JsonSerializer.Serialize(writer, IndexValue, options);
			writer.WritePropertyName("shard");
			writer.WriteNumberValue(ShardValue);
			writer.WritePropertyName("node");
			writer.WriteStringValue(NodeValue);
			writer.WritePropertyName("accept_data_loss");
			writer.WriteBooleanValue(AcceptDataLossValue);
			writer.WriteEndObject();
		}
	}
}
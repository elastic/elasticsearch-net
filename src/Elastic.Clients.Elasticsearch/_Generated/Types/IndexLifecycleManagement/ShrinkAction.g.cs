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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

public sealed partial class ShrinkAction
{
	[JsonInclude, JsonPropertyName("allow_write_after_shrink")]
	public bool? AllowWriteAfterShrink { get; set; }
	[JsonInclude, JsonPropertyName("max_primary_shard_size")]
	public Elastic.Clients.Elasticsearch.ByteSize? MaxPrimaryShardSize { get; set; }
	[JsonInclude, JsonPropertyName("number_of_shards")]
	public int? NumberOfShards { get; set; }
}

public sealed partial class ShrinkActionDescriptor : SerializableDescriptor<ShrinkActionDescriptor>
{
	internal ShrinkActionDescriptor(Action<ShrinkActionDescriptor> configure) => configure.Invoke(this);

	public ShrinkActionDescriptor() : base()
	{
	}

	private bool? AllowWriteAfterShrinkValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxPrimaryShardSizeValue { get; set; }
	private int? NumberOfShardsValue { get; set; }

	public ShrinkActionDescriptor AllowWriteAfterShrink(bool? allowWriteAfterShrink = true)
	{
		AllowWriteAfterShrinkValue = allowWriteAfterShrink;
		return Self;
	}

	public ShrinkActionDescriptor MaxPrimaryShardSize(Elastic.Clients.Elasticsearch.ByteSize? maxPrimaryShardSize)
	{
		MaxPrimaryShardSizeValue = maxPrimaryShardSize;
		return Self;
	}

	public ShrinkActionDescriptor NumberOfShards(int? numberOfShards)
	{
		NumberOfShardsValue = numberOfShards;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowWriteAfterShrinkValue.HasValue)
		{
			writer.WritePropertyName("allow_write_after_shrink");
			writer.WriteBooleanValue(AllowWriteAfterShrinkValue.Value);
		}

		if (MaxPrimaryShardSizeValue is not null)
		{
			writer.WritePropertyName("max_primary_shard_size");
			JsonSerializer.Serialize(writer, MaxPrimaryShardSizeValue, options);
		}

		if (NumberOfShardsValue.HasValue)
		{
			writer.WritePropertyName("number_of_shards");
			writer.WriteNumberValue(NumberOfShardsValue.Value);
		}

		writer.WriteEndObject();
	}
}
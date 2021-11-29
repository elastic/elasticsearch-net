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
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class SamplerAggregation : Aggregations.BucketAggregationBase, IAggregationContainerVariant
	{
		public SamplerAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "sampler";
		[JsonInclude]
		[JsonPropertyName("shard_size")]
		public int? ShardSize { get; set; }
	}

	public sealed partial class SamplerAggregationDescriptor : DescriptorBase<SamplerAggregationDescriptor>
	{
		public SamplerAggregationDescriptor()
		{
		}

		internal SamplerAggregationDescriptor(Action<SamplerAggregationDescriptor> configure) => configure.Invoke(this);
		internal int? ShardSizeValue { get; private set; }

		public SamplerAggregationDescriptor ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSizeValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ShardSizeValue.HasValue)
			{
				writer.WritePropertyName("shard_size");
				writer.WriteNumberValue(ShardSizeValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}
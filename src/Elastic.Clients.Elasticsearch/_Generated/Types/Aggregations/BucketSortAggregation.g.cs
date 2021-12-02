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
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class BucketSortAggregation : Aggregations.AggregationBase, IAggregationContainerVariant
	{
		public BucketSortAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "bucket_sort";
		[JsonInclude]
		[JsonPropertyName("from")]
		public int? From { get; set; }

		[JsonInclude]
		[JsonPropertyName("gap_policy")]
		public Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicy { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }

		[JsonInclude]
		[JsonPropertyName("sort")]
		public Elastic.Clients.Elasticsearch.Sort? Sort { get; set; }
	}

	public sealed partial class BucketSortAggregationDescriptor : DescriptorBase<BucketSortAggregationDescriptor>
	{
		public BucketSortAggregationDescriptor()
		{
		}

		internal BucketSortAggregationDescriptor(Action<BucketSortAggregationDescriptor> configure) => configure.Invoke(this);
		internal int? FromValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicyValue { get; private set; }

		internal int? SizeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Sort? SortValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		public BucketSortAggregationDescriptor From(int? from) => Assign(from, (a, v) => a.FromValue = v);
		public BucketSortAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? gapPolicy) => Assign(gapPolicy, (a, v) => a.GapPolicyValue = v);
		public BucketSortAggregationDescriptor Size(int? size) => Assign(size, (a, v) => a.SizeValue = v);
		public BucketSortAggregationDescriptor Sort(Elastic.Clients.Elasticsearch.Sort? sort) => Assign(sort, (a, v) => a.SortValue = v);
		public BucketSortAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("bucket_sort");
			writer.WriteStartObject();
			if (FromValue.HasValue)
			{
				writer.WritePropertyName("from");
				writer.WriteNumberValue(FromValue.Value);
			}

			if (GapPolicyValue is not null)
			{
				writer.WritePropertyName("gap_policy");
				JsonSerializer.Serialize(writer, GapPolicyValue, options);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			if (SortValue is not null)
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, SortValue, options);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
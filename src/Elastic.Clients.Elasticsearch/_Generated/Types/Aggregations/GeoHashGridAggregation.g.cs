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
	public partial class GeoHashGridAggregation : Aggregations.BucketAggregationBase, IAggregationContainerVariant
	{
		public GeoHashGridAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "geohash_grid";
		[JsonInclude]
		[JsonPropertyName("bounds")]
		public Elastic.Clients.Elasticsearch.GeoBounds? Bounds { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("precision")]
		public Elastic.Clients.Elasticsearch.GeoHashPrecision? Precision { get; set; }

		[JsonInclude]
		[JsonPropertyName("shard_size")]
		public int? ShardSize { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }
	}

	public sealed partial class GeoHashGridAggregationDescriptor<T> : DescriptorBase<GeoHashGridAggregationDescriptor<T>>
	{
		public GeoHashGridAggregationDescriptor()
		{
		}

		internal GeoHashGridAggregationDescriptor(Action<GeoHashGridAggregationDescriptor<T>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.GeoBounds? BoundsValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Field? FieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.GeoHashPrecision? PrecisionValue { get; private set; }

		internal int? ShardSizeValue { get; private set; }

		internal int? SizeValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		public GeoHashGridAggregationDescriptor<T> Bounds(Elastic.Clients.Elasticsearch.GeoBounds? bounds) => Assign(bounds, (a, v) => a.BoundsValue = v);
		public GeoHashGridAggregationDescriptor<T> Field(Elastic.Clients.Elasticsearch.Field? field) => Assign(field, (a, v) => a.FieldValue = v);
		public GeoHashGridAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.FieldValue = v);
		public GeoHashGridAggregationDescriptor<T> Precision(Elastic.Clients.Elasticsearch.GeoHashPrecision? precision) => Assign(precision, (a, v) => a.PrecisionValue = v);
		public GeoHashGridAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSizeValue = v);
		public GeoHashGridAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.SizeValue = v);
		public GeoHashGridAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("geohash_grid");
			writer.WriteStartObject();
			if (BoundsValue is not null)
			{
				writer.WritePropertyName("bounds");
				JsonSerializer.Serialize(writer, BoundsValue, options);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (PrecisionValue is not null)
			{
				writer.WritePropertyName("precision");
				JsonSerializer.Serialize(writer, PrecisionValue, options);
			}

			if (ShardSizeValue.HasValue)
			{
				writer.WritePropertyName("shard_size");
				writer.WriteNumberValue(ShardSizeValue.Value);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
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
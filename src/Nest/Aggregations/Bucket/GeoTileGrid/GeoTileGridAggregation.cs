// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoTileGridAggregation))]
	public interface IGeoTileGridAggregation : IBucketAggregation
	{
		/// <summary>
		/// The name of the field indexed with GeoPoints.
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The zoom of the key used to define cells/buckets in the results.
		/// </summary>
		[DataMember(Name ="precision")]
		GeoTilePrecision? Precision { get; set; }

		/// <summary>
		/// To allow for more accurate counting of the top cells returned in the final result the aggregation.
		/// </summary>
		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }

		/// <summary>
		/// The maximum number of geohash buckets to return.
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }
	}

	public class GeoTileGridAggregation : BucketAggregationBase, IGeoTileGridAggregation
	{
		internal GeoTileGridAggregation() { }

		public GeoTileGridAggregation(string name) : base(name) { }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public GeoTilePrecision? Precision { get; set; }

		/// <inheritdoc />
		public int? ShardSize { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoTile = this;
	}

	public class GeoTileGridAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GeoTileGridAggregationDescriptor<T>, IGeoTileGridAggregation, T>
			, IGeoTileGridAggregation
		where T : class
	{
		Field IGeoTileGridAggregation.Field { get; set; }

		GeoTilePrecision? IGeoTileGridAggregation.Precision { get; set; }

		int? IGeoTileGridAggregation.ShardSize { get; set; }

		int? IGeoTileGridAggregation.Size { get; set; }

		/// <inheritdoc cref="IGeoTileGridAggregation.Field" />
		public GeoTileGridAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IGeoTileGridAggregation.Field" />
		public GeoTileGridAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IGeoTileGridAggregation.Size" />
		public GeoTileGridAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IGeoTileGridAggregation.ShardSize" />
		public GeoTileGridAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		/// <inheritdoc cref="IGeoTileGridAggregation.Precision" />
		public GeoTileGridAggregationDescriptor<T> Precision(GeoTilePrecision? precision) =>
			Assign(precision, (a, v) => a.Precision = v);
	}
}

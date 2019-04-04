using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<GeoHashGridAggregation>))]
	public interface IGeoHashGridAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("precision")]
		GeoHashPrecision? Precision { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }
	}

	public class GeoHashGridAggregation : BucketAggregationBase, IGeoHashGridAggregation
	{
		internal GeoHashGridAggregation() { }

		public GeoHashGridAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public GeoHashPrecision? Precision { get; set; }
		public int? ShardSize { get; set; }
		public int? Size { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoHash = this;
	}

	public class GeoHashGridAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GeoHashGridAggregationDescriptor<T>, IGeoHashGridAggregation, T>
			, IGeoHashGridAggregation
		where T : class
	{
		Field IGeoHashGridAggregation.Field { get; set; }

		GeoHashPrecision? IGeoHashGridAggregation.Precision { get; set; }

		int? IGeoHashGridAggregation.ShardSize { get; set; }

		int? IGeoHashGridAggregation.Size { get; set; }

		public GeoHashGridAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public GeoHashGridAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(field, (a, v) => a.Field = v);

		public GeoHashGridAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public GeoHashGridAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		public GeoHashGridAggregationDescriptor<T> GeoHashPrecision(GeoHashPrecision? precision) =>
			Assign(precision, (a, v) => a.Precision = v);
	}
}

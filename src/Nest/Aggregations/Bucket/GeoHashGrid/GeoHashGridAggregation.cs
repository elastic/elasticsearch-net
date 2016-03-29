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

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("precision")]
		GeoHashPrecision? Precision { get; set; }
	}

	public class GeoHashGridAggregation : BucketAggregationBase, IGeoHashGridAggregation
	{
		public Field Field { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public GeoHashPrecision? Precision { get; set; }

		internal GeoHashGridAggregation() { }

		public GeoHashGridAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoHash = this;
	}

	public class GeoHashGridAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GeoHashGridAggregationDescriptor<T>, IGeoHashGridAggregation, T>
			, IGeoHashGridAggregation
		where T : class
	{
		Field IGeoHashGridAggregation.Field { get; set; }

		int? IGeoHashGridAggregation.Size { get; set; }

		int? IGeoHashGridAggregation.ShardSize { get; set; }

		GeoHashPrecision? IGeoHashGridAggregation.Precision { get; set; }

		public GeoHashGridAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GeoHashGridAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoHashGridAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public GeoHashGridAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public GeoHashGridAggregationDescriptor<T> GeoHashPrecision(GeoHashPrecision precision) =>
			Assign(a => a.Precision = precision);
	}
}

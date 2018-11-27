using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoHashGridAggregation))]
	public interface IGeoHashGridAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="precision")]
		GeoHashPrecision? Precision { get; set; }

		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }

		[DataMember(Name ="size")]
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

		public GeoHashGridAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GeoHashGridAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoHashGridAggregationDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public GeoHashGridAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(a => a.ShardSize = shardSize);

		public GeoHashGridAggregationDescriptor<T> GeoHashPrecision(GeoHashPrecision? precision) =>
			Assign(a => a.Precision = precision);
	}
}

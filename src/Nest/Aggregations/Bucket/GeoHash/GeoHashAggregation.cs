using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GeoHashAggregation>))]
	public interface IGeoHashAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		FieldName Field { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("precision")]
		GeoHashPrecision? Precision { get; set; }
	}

	public class GeoHashAggregation : BucketAggregation, IGeoHashAggregation
	{
		public FieldName Field { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public GeoHashPrecision? Precision { get; set; }

		public GeoHashAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoHash = this;
	}

	public class GeoHashAggregationDescriptor<T> 
		: BucketAggregationDescriptorBase<GeoHashAggregationDescriptor<T>, IGeoHashAggregation, T>
			, IGeoHashAggregation 
		where T : class
	{
		FieldName IGeoHashAggregation.Field { get; set; }
		
		int? IGeoHashAggregation.Size { get; set; }

		int? IGeoHashAggregation.ShardSize { get; set; }

		GeoHashPrecision? IGeoHashAggregation.Precision { get; set; }

		public GeoHashAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public GeoHashAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoHashAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public GeoHashAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public GeoHashAggregationDescriptor<T> GeoHashPrecision(GeoHashPrecision precision) =>
			Assign(a => a.Precision = precision);
	}
}
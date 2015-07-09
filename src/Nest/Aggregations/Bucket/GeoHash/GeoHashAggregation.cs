using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<GeoHashAggregator>))]
	public interface IGeoHashAggregator : IBucketAggregator
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

	public class GeoHashAggregator : BucketAggregator, IGeoHashAggregator
	{
		public FieldName Field { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public GeoHashPrecision? Precision { get; set; }
	}

	public class GeoHashAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<GeoHashAggregatorDescriptor<T>, IGeoHashAggregator, T>
			, IGeoHashAggregator 
		where T : class
	{
		FieldName IGeoHashAggregator.Field { get; set; }
		
		int? IGeoHashAggregator.Size { get; set; }

		int? IGeoHashAggregator.ShardSize { get; set; }

		GeoHashPrecision? IGeoHashAggregator.Precision { get; set; }

		public GeoHashAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public GeoHashAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoHashAggregatorDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public GeoHashAggregatorDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public GeoHashAggregatorDescriptor<T> GeoHashPrecision(GeoHashPrecision precision) =>
			Assign(a => a.Precision = precision);

	}
}
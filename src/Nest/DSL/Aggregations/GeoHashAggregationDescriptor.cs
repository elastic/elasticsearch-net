using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class GeoHashAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GeoHashAggregationDescriptor<T>, T>
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public GeoHashAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public GeoHashAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}
		
		[JsonProperty("size")]
		internal int? _Size { get; set; }

		public GeoHashAggregationDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}
		
		[JsonProperty("shard_size")]
		internal int? _ShardSize { get; set; }

		public GeoHashAggregationDescriptor<T> ShardSize(int shardSize)
		{
			this._ShardSize = shardSize;
			return this;
		}

		[JsonProperty("precision")]
		internal GeoHashPrecision? _Precision { get; set; }
	
		public GeoHashAggregationDescriptor<T> GeoHashPrecision(GeoHashPrecision precision)
		{
			this._Precision = precision;
			return this;
		}
	}
}
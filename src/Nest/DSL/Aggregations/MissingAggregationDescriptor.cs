using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MissingAggregator>))]
	public interface IMissingAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }
	}

	public class MissingAggregator : BucketAggregator, IMissingAggregator
	{
		public PropertyPathMarker Field { get; set; }
	}

	public class MissingAggregationDescriptor<T> : BucketAggregationBaseDescriptor<MissingAggregationDescriptor<T>, T>, IMissingAggregator where T : class
	{
		PropertyPathMarker IMissingAggregator.Field { get; set; }
		
		public MissingAggregationDescriptor<T> Field(string field)
		{
			((IMissingAggregator)this).Field = field;
			return this;
		}

		public MissingAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			((IMissingAggregator)this).Field = field;
			return this;
		}
	}
}
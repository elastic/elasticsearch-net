using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ReverseNestedAggregator>))]
	public interface IReverseNestedAggregator : IBucketAggregator
	{
		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }
	}

	public class ReverseNestedAggregator : BucketAggregator, IReverseNestedAggregator
	{
		[JsonProperty("path")]
		public PropertyPathMarker Path { get; set; }
	}

	public class ReverseNestedAggregationDescriptor<T> 
		: BucketAggregationBaseDescriptor<ReverseNestedAggregationDescriptor<T>, T>, IReverseNestedAggregator 
		where T : class
	{
		IReverseNestedAggregator Self => this;
		PropertyPathMarker IReverseNestedAggregator.Path { get; set; }

		public ReverseNestedAggregationDescriptor<T> Path(string path)
		{
			this.Self.Path = path;
			return this;
		}

		public ReverseNestedAggregationDescriptor<T> Path(Expression<Func<T, object>> path)
		{
			this.Self.Path = path;
			return this;
		}
	}
}

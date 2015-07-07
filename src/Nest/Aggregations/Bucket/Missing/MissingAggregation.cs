using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MissingAggregator>))]
	public interface IMissingAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPath Field { get; set; }
	}

	public class MissingAggregator : BucketAggregator, IMissingAggregator
	{
		public PropertyPath Field { get; set; }
	}

	public class MissingAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<MissingAggregatorDescriptor<T>,IMissingAggregator, T>
			, IMissingAggregator 
		where T : class
	{
		PropertyPath IMissingAggregator.Field { get; set; }

		public MissingAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public MissingAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

	}
}
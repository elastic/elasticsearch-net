using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<RangeAggregator>))]
	public interface IRangeAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		FluentDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class RangeAggregator : BucketAggregator, IRangeAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Script { get; set; }
		public FluentDictionary<string, object> Params { get; set; }
		public IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class RangeAggregationDescriptor<T> : BucketAggregationBaseDescriptor<RangeAggregationDescriptor<T>, T>, IRangeAggregator 
		where T : class 
	{
		private IRangeAggregator Self { get { return this; } }

		PropertyPathMarker IRangeAggregator.Field { get; set; }
		
		string IRangeAggregator.Script { get; set; }

		FluentDictionary<string, object> IRangeAggregator.Params { get; set; }

		IEnumerable<Range<double>> IRangeAggregator.Ranges { get; set; }
		
		public RangeAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public RangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public RangeAggregationDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public RangeAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self.Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		public RangeAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges)
		{
			var newRanges = from range in ranges let r = new Range<double>() select range(r);
			Self.Ranges = newRanges;
			return this;
		}
	}
}
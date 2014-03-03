using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class RangeAggregationDescriptor<T> : BucketAggregationBaseDescriptor<RangeAggregationDescriptor<T>, T>
		where T : class 
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public RangeAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public RangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("script")]
		internal string _Script { get; set; }

		public RangeAggregationDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}

		[JsonProperty("params")]
		internal FluentDictionary<string, object> _Params { get; set; }

		public RangeAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this._Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty(PropertyName = "ranges")]
		internal IEnumerable<Range<double>> _Ranges { get; set; }
		
		public RangeAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges)
		{
			var newRanges = from range in ranges let r = new Range<double>() select range(r);
			this._Ranges = newRanges;
			return this;
		}
	}
}
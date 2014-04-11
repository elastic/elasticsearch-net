using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class PercentilesAggregationDescriptor<T> : BucketAggregationBaseDescriptor<PercentilesAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public PercentilesAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public PercentilesAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("script")]
		internal string _Script { get; set; }

		public PercentilesAggregationDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}

		[JsonProperty("params")]
		internal FluentDictionary<string, object> _Params { get; set; }

		public PercentilesAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this._Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty("percents")]
		internal IEnumerable<double> _Percentages { get; set; }

		public PercentilesAggregationDescriptor<T> Percentages(params double[] percentages)
		{
			this._Percentages = percentages;
			return this;
		}
		
		[JsonProperty("compression")]
		internal int? _Compression { get; set; }

		public PercentilesAggregationDescriptor<T> Compression(int compression)
		{
			this._Compression = compression;
			return this;
		}
	}
}
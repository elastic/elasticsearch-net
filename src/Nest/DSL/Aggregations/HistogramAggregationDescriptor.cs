using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class HistogramAggregationDescriptor<T> : MetricAggregationBaseDescriptor<HistogramAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public HistogramAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public HistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("script")]
		internal string _Script { get; set; }

		public HistogramAggregationDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}

		[JsonProperty("params")]
		internal FluentDictionary<string, object> _Params { get; set; }

		public HistogramAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this._Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty("interval")]
		internal double? _Interval { get; set; }

		public HistogramAggregationDescriptor<T> Interval(double interval)
		{
			this._Interval = interval;
			return this;
		}
		
		[JsonProperty("min_doc_count")]
		internal int? _MinimumDocumentCount { get; set; }

		public HistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			this._MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		[JsonProperty("order")]
		internal IDictionary<string, string> _Order { get; set; }

		public HistogramAggregationDescriptor<T> OrderAscending(string key)
		{
			this._Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}
	
		public HistogramAggregationDescriptor<T> OrderDescending(string key)
		{
			this._Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}

	}
}
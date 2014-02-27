using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{

	public class AggregationDescriptor<T>
		where T : class
	{
		private readonly IDictionary<string, IAggregationDescriptor> _aggregations =
			new Dictionary<string, IAggregationDescriptor>();

		public AggregationDescriptor<T> Average(
			Func<AverageAggregationDescriptor<T>, AverageAggregationDescriptor<T>> selector)
		{
			var agg = selector(new AverageAggregationDescriptor<T>());
			if (agg == null) return this;
			this._aggregations.Add("avg", agg);
			return this;
		}
	}

	public interface IAggregationDescriptor
	{
		
	}

	public abstract class MetricAggregationBaseDescriptor<TMetricAggregation, T> : IAggregationDescriptor
		where TMetricAggregation : MetricAggregationBaseDescriptor<TMetricAggregation, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public TMetricAggregation Field(string field)
		{
			this._Field = field;
			return (TMetricAggregation)this;
		}

		public TMetricAggregation Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return (TMetricAggregation) this;
		}

		[JsonProperty("script")]
		internal string _Script { get; set; }

		public TMetricAggregation Script(string script)
		{
			this._Script = script;
			return (TMetricAggregation)this;
		}

		[JsonProperty("params")]
		internal FluentDictionary<string, object> _Params { get; set; }

		public TMetricAggregation Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this._Params = paramSelector(new FluentDictionary<string, object>());
			return (TMetricAggregation) this;
		}
	}

	public abstract class BucketAggregationBaseDescriptor<TBucketAggregation, T>: IAggregationDescriptor
		where TBucketAggregation : BucketAggregationBaseDescriptor<TBucketAggregation, T>
		where T : class
	{
		[JsonProperty("aggs")] 
		internal AggregationDescriptor<T> _Aggregations;

		public TBucketAggregation Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> selector)
		{
			this._Aggregations = selector(new AggregationDescriptor<T>());
			return (TBucketAggregation)this;
		}

	}
}

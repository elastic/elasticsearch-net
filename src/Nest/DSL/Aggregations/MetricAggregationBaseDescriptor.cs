using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMetricAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }
	}
	
	public abstract class MetricAggregator : IMetricAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
	}

	public abstract class MetricAggregationBaseDescriptor<TMetricAggregation, T> : IAggregationDescriptor, IMetricAggregator 
		where TMetricAggregation : MetricAggregationBaseDescriptor<TMetricAggregation, T>
		where T : class
	{
		private IMetricAggregator Self { get { return this;  } }

		PropertyPathMarker IMetricAggregator.Field { get; set; }
		
		string IMetricAggregator.Script { get; set; }

		IDictionary<string, object> IMetricAggregator.Params { get; set; }

		public TMetricAggregation Field(string field)
		{
			Self.Field = field;
			return (TMetricAggregation)this;
		}

		public TMetricAggregation Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return (TMetricAggregation) this;
		}

		public TMetricAggregation Script(string script)
		{
			Self.Script = script;
			return (TMetricAggregation)this;
		}

		public TMetricAggregation Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self.Params = paramSelector(new FluentDictionary<string, object>());
			return (TMetricAggregation) this;
		}
	}
}

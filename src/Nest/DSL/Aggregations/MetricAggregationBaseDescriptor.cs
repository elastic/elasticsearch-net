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
		PropertyPathMarker _Field { get; set; }

		[JsonProperty("script")]
		string _Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> _Params { get; set; }
	}
	
	public abstract class MetricAggregator : IMetricAggregator
	{
		public PropertyPathMarker _Field { get; set; }
		public string _Script { get; set; }
		public IDictionary<string, object> _Params { get; set; }
	}

	public abstract class MetricAggregationBaseDescriptor<TMetricAggregation, T> : IAggregationDescriptor, IMetricAggregator 
		where TMetricAggregation : MetricAggregationBaseDescriptor<TMetricAggregation, T>
		where T : class
	{
		private IMetricAggregator Self { get { return this;  } }

		PropertyPathMarker IMetricAggregator._Field { get; set; }
		
		string IMetricAggregator._Script { get; set; }

		IDictionary<string, object> IMetricAggregator._Params { get; set; }

		public TMetricAggregation Field(string field)
		{
			Self._Field = field;
			return (TMetricAggregation)this;
		}

		public TMetricAggregation Field(Expression<Func<T, object>> field)
		{
			Self._Field = field;
			return (TMetricAggregation) this;
		}

		public TMetricAggregation Script(string script)
		{
			Self._Script = script;
			return (TMetricAggregation)this;
		}

		public TMetricAggregation Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self._Params = paramSelector(new FluentDictionary<string, object>());
			return (TMetricAggregation) this;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMetricAggregator : IAggregator
	{
		[JsonProperty("field")]
		FieldName Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Language { get; set; }

	}
	
	public abstract class MetricAggregator : IMetricAggregator
	{
		public FieldName Field { get; set; }
		public virtual string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public string Language { get; set; }
	}

	public abstract class MetricAgg : AggregatorBase, IMetricAggregator
	{
		FieldName IMetricAggregator.Field { get; set; }

		protected MetricAgg(string name, FieldName field) : base(name)
		{
			((IMetricAggregator)this).Field = field;
		}

		public virtual string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public string Language { get; set; }
	}

	public abstract class MetricAggregationBaseDescriptor<TMetricAggregation, TMetricAggregationInterface, T> 
		:  IMetricAggregator 
		where TMetricAggregation : MetricAggregationBaseDescriptor<TMetricAggregation, TMetricAggregationInterface, T>
			, TMetricAggregationInterface, IMetricAggregator 
		where T : class
		where TMetricAggregationInterface : class, IMetricAggregator
	{
		protected TMetricAggregation Assign(Action<TMetricAggregationInterface> assigner) =>
			Fluent.Assign(((TMetricAggregation)this), assigner);

		protected TMetricAggregationInterface Self => (TMetricAggregation)this;

		FieldName IMetricAggregator.Field { get; set; }
		
		string IMetricAggregator.Script { get; set; }

		IDictionary<string, object> IMetricAggregator.Params { get; set; }

		string IMetricAggregator.Language { get; set; }

		public TMetricAggregation Field(string field) => Assign(a => a.Field = field);

		public TMetricAggregation Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public virtual TMetricAggregation Script(string script) => Assign(a => a.Script = script);

		public TMetricAggregation Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
				Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public TMetricAggregation Language(string language) => Assign(a => a.Language = language);

	}
}

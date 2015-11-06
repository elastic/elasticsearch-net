using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMetricAggregation : IAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Language { get; set; }
	}
	
	public abstract class MetricAggregationBase : AggregationBase, IMetricAggregation
	{
		internal MetricAggregationBase() { }

		protected MetricAggregationBase(string name, Field field) : base(name)
		{
			this.Field = field;
		}

		public Field Field { get; set; }
		public virtual string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public string Language { get; set; }
	}

	public abstract class MetricAggregationDescriptorBase<TMetricAggregation, TMetricAggregationInterface, T> 
		:  IMetricAggregation 
		where TMetricAggregation : MetricAggregationDescriptorBase<TMetricAggregation, TMetricAggregationInterface, T>
			, TMetricAggregationInterface, IMetricAggregation 
		where T : class
		where TMetricAggregationInterface : class, IMetricAggregation
	{
		protected TMetricAggregation Assign(Action<TMetricAggregationInterface> assigner) =>
			Fluent.Assign(((TMetricAggregation)this), assigner);

		protected TMetricAggregationInterface Self => (TMetricAggregation)this;

		Field IMetricAggregation.Field { get; set; }
		
		string IMetricAggregation.Script { get; set; }

		IDictionary<string, object> IMetricAggregation.Params { get; set; }

		string IMetricAggregation.Language { get; set; }

		public TMetricAggregation Field(string field) => Assign(a => a.Field = field);

		public TMetricAggregation Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public virtual TMetricAggregation Script(string script) => Assign(a => a.Script = script);

		public TMetricAggregation Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
				Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public TMetricAggregation Language(string language) => Assign(a => a.Language = language);

	}
}

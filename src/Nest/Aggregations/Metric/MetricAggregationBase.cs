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
		IScript Script { get; set; }

		[JsonProperty("missing")]
		double? Missing { get; set; }
	}
	
	public abstract class MetricAggregationBase : AggregationBase, IMetricAggregation
	{
		internal MetricAggregationBase() { }

		protected MetricAggregationBase(string name, Field field) : base(name)
		{
			this.Field = field;
		}

		public Field Field { get; set; }
		public virtual IScript Script { get; set; }
		public double? Missing { get; set; }
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
		
		IScript IMetricAggregation.Script { get; set; }

		double? IMetricAggregation.Missing { get; set; }

		public TMetricAggregation Field(string field) => Assign(a => a.Field = field);

		public TMetricAggregation Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public virtual TMetricAggregation Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public virtual TMetricAggregation Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public TMetricAggregation Missing(double missing) => Assign(a => a.Missing = missing);
	}
}

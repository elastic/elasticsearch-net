using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMetricAggregation : IAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="missing")]
		double? Missing { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	public abstract class MetricAggregationBase : AggregationBase, IMetricAggregation
	{
		internal MetricAggregationBase() { }

		protected MetricAggregationBase(string name, Field field) : base(name) => Field = field;

		public Field Field { get; set; }
		public double? Missing { get; set; }
		public virtual IScript Script { get; set; }
	}

	public abstract class MetricAggregationDescriptorBase<TMetricAggregation, TMetricAggregationInterface, T>
		: DescriptorBase<TMetricAggregation, TMetricAggregationInterface>, IMetricAggregation
		where TMetricAggregation : MetricAggregationDescriptorBase<TMetricAggregation, TMetricAggregationInterface, T>
		, TMetricAggregationInterface, IMetricAggregation
		where T : class
		where TMetricAggregationInterface : class, IMetricAggregation
	{
		Field IMetricAggregation.Field { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		double? IMetricAggregation.Missing { get; set; }

		string IAggregation.Name { get; set; }

		IScript IMetricAggregation.Script { get; set; }

		public TMetricAggregation Field(Field field) => Assign(a => a.Field = field);

		public TMetricAggregation Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public virtual TMetricAggregation Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public virtual TMetricAggregation Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public TMetricAggregation Missing(double? missing) => Assign(a => a.Missing = missing);

		public TMetricAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}

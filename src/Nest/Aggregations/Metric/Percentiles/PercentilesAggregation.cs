using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(PercentilesAggregationFormatter))]
	public interface IPercentilesAggregation : IMetricAggregation
	{
		IPercentilesMethod Method { get; set; }
		IEnumerable<double> Percents { get; set; }
		bool? Keyed { get; set; }
	}

	public class PercentilesAggregation : MetricAggregationBase, IPercentilesAggregation
	{
		internal PercentilesAggregation() { }

		public PercentilesAggregation(string name, Field field) : base(name, field) { }

		public IPercentilesMethod Method { get; set; }
		public IEnumerable<double> Percents { get; set; }
		public bool? Keyed { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Percentiles = this;
	}

	public class PercentilesAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<PercentilesAggregationDescriptor<T>, IPercentilesAggregation, T>
			, IPercentilesAggregation
		where T : class
	{
		IPercentilesMethod IPercentilesAggregation.Method { get; set; }
		IEnumerable<double> IPercentilesAggregation.Percents { get; set; }
		bool? IPercentilesAggregation.Keyed { get; set; }

		public PercentilesAggregationDescriptor<T> Percents(IEnumerable<double> percentages) =>
			Assign(percentages, (a, v) => a.Percents = v);

		public PercentilesAggregationDescriptor<T> Percents(params double[] percentages) =>
			Assign(percentages, (a, v) => a.Percents = v);

		public PercentilesAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelector) =>
			Assign(methodSelector, (a, v) => a.Method = v?.Invoke(new PercentilesMethodDescriptor()));

		public PercentilesAggregationDescriptor<T> Keyed(bool? keyed = true) =>
			Assign(keyed, (a, v) => a.Keyed = v);
	}
}

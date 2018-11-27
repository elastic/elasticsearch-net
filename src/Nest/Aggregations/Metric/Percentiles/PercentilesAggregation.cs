using System;
using System.Collections.Generic;
using System.Linq;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(PercentilesAggregationFormatter))]
	public interface IPercentilesAggregation : IMetricAggregation
	{
		IPercentilesMethod Method { get; set; }
		IEnumerable<double> Percents { get; set; }
	}

	public class PercentilesAggregation : MetricAggregationBase, IPercentilesAggregation
	{
		internal PercentilesAggregation() { }

		public PercentilesAggregation(string name, Field field) : base(name, field) { }

		public IPercentilesMethod Method { get; set; }
		public IEnumerable<double> Percents { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Percentiles = this;
	}

	public class PercentilesAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<PercentilesAggregationDescriptor<T>, IPercentilesAggregation, T>
			, IPercentilesAggregation
		where T : class
	{
		IPercentilesMethod IPercentilesAggregation.Method { get; set; }
		IEnumerable<double> IPercentilesAggregation.Percents { get; set; }

		public PercentilesAggregationDescriptor<T> Percents(IEnumerable<double> percentages) =>
			Assign(a => a.Percents = percentages?.ToList());

		public PercentilesAggregationDescriptor<T> Percents(params double[] percentages) =>
			Assign(a => a.Percents = percentages?.ToList());

		public PercentilesAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelector) =>
			Assign(a => a.Method = methodSelector?.Invoke(new PercentilesMethodDescriptor()));
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(PercentileRanksAggregationJsonConverter))]
	public interface IPercentileRanksAggregation : IMetricAggregation
	{
		IPercentilesMethod Method { get; set; }
		IEnumerable<double> Values { get; set; }
	}

	public class PercentileRanksAggregation : MetricAggregationBase, IPercentileRanksAggregation
	{
		internal PercentileRanksAggregation() { }

		public PercentileRanksAggregation(string name, Field field) : base(name, field) { }

		public IPercentilesMethod Method { get; set; }
		public IEnumerable<double> Values { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentileRanks = this;
	}

	public class PercentileRanksAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<PercentileRanksAggregationDescriptor<T>, IPercentileRanksAggregation, T>, IPercentileRanksAggregation
		where T : class
	{
		IPercentilesMethod IPercentileRanksAggregation.Method { get; set; }
		IEnumerable<double> IPercentileRanksAggregation.Values { get; set; }

		public PercentileRanksAggregationDescriptor<T> Values(IEnumerable<double> values) =>
			Assign(values, (a, v) => a.Values = v);

		public PercentileRanksAggregationDescriptor<T> Values(params double[] values) =>
			Assign(values, (a, v) => a.Values = v);

		public PercentileRanksAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelctor) =>
			Assign(methodSelctor, (a, v) => a.Method = v?.Invoke(new PercentilesMethodDescriptor()));
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(PercentileRanksAggregationJsonConverter))]
	public interface IPercentileRanksAggregation : IMetricAggregation
	{
		IEnumerable<double> Values { get; set; }
		IPercentilesMethod Method { get; set; }
	}

	public class PercentileRanksAggregation : MetricAggregationBase, IPercentileRanksAggregation
	{
		public IEnumerable<double> Values { get; set; }
		public IPercentilesMethod Method { get; set; }

		internal PercentileRanksAggregation() { }

		public PercentileRanksAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentileRanks = this;
	}

	public class PercentileRanksAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<PercentileRanksAggregationDescriptor<T>, IPercentileRanksAggregation, T>, IPercentileRanksAggregation
		where T : class
	{
		IEnumerable<double> IPercentileRanksAggregation.Values { get; set; }

		IPercentilesMethod IPercentileRanksAggregation.Method { get; set; }

		public PercentileRanksAggregationDescriptor<T> Values(IEnumerable<double> values) =>
			Assign(a => a.Values = values?.ToList());

		public PercentileRanksAggregationDescriptor<T> Values(params double[] values) =>
			Assign(a => a.Values = values?.ToList());

		public PercentileRanksAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelctor) =>
			Assign(a => a.Method = methodSelctor?.Invoke(new PercentilesMethodDescriptor()));
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(PercentileRanksAggregationFormatter))]
	public interface IPercentileRanksAggregation : IMetricAggregation
	{
		IPercentilesMethod Method { get; set; }
		IEnumerable<double> Values { get; set; }
		bool? Keyed { get; set; }
	}

	public class PercentileRanksAggregation : MetricAggregationBase, IPercentileRanksAggregation
	{
		internal PercentileRanksAggregation() { }

		public PercentileRanksAggregation(string name, Field field) : base(name, field) { }

		public IPercentilesMethod Method { get; set; }
		public IEnumerable<double> Values { get; set; }
		public bool? Keyed { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentileRanks = this;
	}

	public class PercentileRanksAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<PercentileRanksAggregationDescriptor<T>, IPercentileRanksAggregation, T>, IPercentileRanksAggregation
		where T : class
	{
		IPercentilesMethod IPercentileRanksAggregation.Method { get; set; }
		IEnumerable<double> IPercentileRanksAggregation.Values { get; set; }
		bool? IPercentileRanksAggregation.Keyed { get; set; }

		public PercentileRanksAggregationDescriptor<T> Values(IEnumerable<double> values) =>
			Assign(a => a.Values = values?.ToList());

		public PercentileRanksAggregationDescriptor<T> Values(params double[] values) =>
			Assign(a => a.Values = values?.ToList());

		public PercentileRanksAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelctor) =>
			Assign(a => a.Method = methodSelctor?.Invoke(new PercentilesMethodDescriptor()));

		public PercentileRanksAggregationDescriptor<T> Keyed(bool? keyed = true) =>
			Assign(a => a.Keyed = keyed);
	}
}

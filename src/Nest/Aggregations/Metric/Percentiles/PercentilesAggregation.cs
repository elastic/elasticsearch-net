using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(PercentilesAggregationJsonConverter))]
	public interface IPercentilesAggregation : IMetricAggregation
	{
		IEnumerable<double> Percents { get; set; }
		IPercentilesMethod Method { get; set; }
	}

	public class PercentilesAggregation : MetricAggregationBase, IPercentilesAggregation
	{
		public IEnumerable<double> Percents { get; set; }
		public IPercentilesMethod Method { get; set; }
			
		internal PercentilesAggregation() { }

		public PercentilesAggregation(string name, Field field) : base(name, field) { } 

		internal override void WrapInContainer(AggregationContainer c) => c.Percentiles = this;
	}

	public class PercentilesAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<PercentilesAggregationDescriptor<T>, IPercentilesAggregation, T>
			, IPercentilesAggregation 
		where T : class
	{
		IEnumerable<double> IPercentilesAggregation.Percents { get; set; }

		IPercentilesMethod IPercentilesAggregation.Method { get; set; }

		public PercentilesAggregationDescriptor<T> Percents(IEnumerable<double> percentages) => 
			Assign(a => a.Percents = percentages?.ToList());

		public PercentilesAggregationDescriptor<T> Percents(params double[] percentages) => 
			Assign(a => a.Percents = percentages?.ToList());

		public PercentilesAggregationDescriptor<T> Method(Func<PercentilesMethodDescriptor, IPercentilesMethod> methodSelector) => 
			Assign(a => a.Method = methodSelector?.Invoke(new PercentilesMethodDescriptor()));
	}
}
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SumAggregation))]
	public interface ISumAggregation : IMetricAggregation { }

	public class SumAggregation : MetricAggregationBase, ISumAggregation
	{
		internal SumAggregation() { }

		public SumAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Sum = this;
	}

	public class SumAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<SumAggregationDescriptor<T>, ISumAggregation, T>
			, ISumAggregation
		where T : class { }
}

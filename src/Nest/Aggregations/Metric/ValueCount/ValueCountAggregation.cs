using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ValueCountAggregation))]
	public interface IValueCountAggregation : IMetricAggregation { }

	public class ValueCountAggregation : MetricAggregationBase, IValueCountAggregation
	{
		internal ValueCountAggregation() { }

		public ValueCountAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ValueCount = this;
	}

	public class ValueCountAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ValueCountAggregationDescriptor<T>, IValueCountAggregation, T>
			, IValueCountAggregation
		where T : class { }
}

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ValueCountAggregation))]
	public interface IValueCountAggregation : IFormattableMetricAggregation { }

	public class ValueCountAggregation : FormattableMetricAggregationBase, IValueCountAggregation
	{
		internal ValueCountAggregation() { }

		public ValueCountAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ValueCount = this;
	}

	public class ValueCountAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<ValueCountAggregationDescriptor<T>, IValueCountAggregation, T>
			, IValueCountAggregation
		where T : class { }
}

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MaxAggregation))]
	public interface IMaxAggregation : IFormattableMetricAggregation { }

	public class MaxAggregation : FormattableMetricAggregationBase, IMaxAggregation
	{
		internal MaxAggregation() { }

		public MaxAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Max = this;
	}

	public class MaxAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<MaxAggregationDescriptor<T>, IMaxAggregation, T>
			, IMaxAggregation
		where T : class { }
}

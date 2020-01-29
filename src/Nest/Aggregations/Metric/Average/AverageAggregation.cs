using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AverageAggregation))]
	public interface IAverageAggregation : IFormattableMetricAggregation { }

	public class AverageAggregation : FormattableMetricAggregationBase, IAverageAggregation
	{
		internal AverageAggregation() { }

		public AverageAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Average = this;
	}

	public class AverageAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<AverageAggregationDescriptor<T>, IAverageAggregation, T>
			, IAverageAggregation
		where T : class { }
}

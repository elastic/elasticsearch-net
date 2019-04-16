using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MinAggregation))]
	public interface IMinAggregation : IMetricAggregation { }

	public class MinAggregation : MetricAggregationBase, IMinAggregation
	{
		internal MinAggregation() { }

		public MinAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Min = this;
	}

	public class MinAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<MinAggregationDescriptor<T>, IMinAggregation, T>
			, IMinAggregation
		where T : class { }
}

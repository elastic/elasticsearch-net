using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(CumulativeCardinalityAggregation))]
	public interface ICumulativeCardinalityAggregation : IPipelineAggregation { }

	public class CumulativeCardinalityAggregation
		: PipelineAggregationBase, ICumulativeCardinalityAggregation
	{
		internal CumulativeCardinalityAggregation() { }

		public CumulativeCardinalityAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.CumulativeCardinality = this;
	}

	public class CumulativeCardinalityAggregationDescriptor
		: PipelineAggregationDescriptorBase<CumulativeCardinalityAggregationDescriptor, ICumulativeCardinalityAggregation, SingleBucketsPath>
			, ICumulativeCardinalityAggregation { }
}

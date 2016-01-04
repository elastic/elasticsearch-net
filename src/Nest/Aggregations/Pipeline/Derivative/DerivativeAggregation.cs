using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<DerivativeAggregation>))]
	public interface IDerivativeAggregation : IPipelineAggregation { }

	public class DerivativeAggregation : PipelineAggregationBase, IDerivativeAggregation
	{
		internal DerivativeAggregation() { }

		public DerivativeAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Derivative = this;
	}

	public class DerivativeAggregationDescriptor
		: PipelineAggregationDescriptorBase<DerivativeAggregationDescriptor, IDerivativeAggregation, SingleBucketsPath>
		, IDerivativeAggregation { }
}

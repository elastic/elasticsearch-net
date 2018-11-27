using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ExtendedStatsBucketAggregation))]
	public interface IExtendedStatsBucketAggregation : IPipelineAggregation
	{
		[DataMember(Name ="sigma")]
		double? Sigma { get; set; }
	}

	public class ExtendedStatsBucketAggregation
		: PipelineAggregationBase, IExtendedStatsBucketAggregation
	{
		internal ExtendedStatsBucketAggregation() { }

		public ExtendedStatsBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public double? Sigma { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStatsBucket = this;
	}

	public class ExtendedStatsBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<ExtendedStatsBucketAggregationDescriptor, IExtendedStatsBucketAggregation, SingleBucketsPath>
			, IExtendedStatsBucketAggregation
	{
		double? IExtendedStatsBucketAggregation.Sigma { get; set; }

		public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma) => Assign(a => a.Sigma = sigma);
	}
}

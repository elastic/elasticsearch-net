using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(PercentilesBucketAggregation))]
	public interface IPercentilesBucketAggregation : IPipelineAggregation
	{
		[DataMember(Name ="percents")]
		IEnumerable<double> Percents { get; set; }
	}

	public class PercentilesBucketAggregation
		: PipelineAggregationBase, IPercentilesBucketAggregation
	{
		internal PercentilesBucketAggregation() { }

		public PercentilesBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public IEnumerable<double> Percents { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentilesBucket = this;
	}

	public class PercentilesBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<PercentilesBucketAggregationDescriptor, IPercentilesBucketAggregation, SingleBucketsPath>
			, IPercentilesBucketAggregation
	{
		IEnumerable<double> IPercentilesBucketAggregation.Percents { get; set; }

		public PercentilesBucketAggregationDescriptor Percents(IEnumerable<double> percentages) =>
			Assign(a => a.Percents = percentages?.ToList());

		public PercentilesBucketAggregationDescriptor Percents(params double[] percentages) =>
			Assign(a => a.Percents = percentages?.ToList());
	}
}

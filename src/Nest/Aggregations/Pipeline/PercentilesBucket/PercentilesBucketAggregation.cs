using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
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
			Assign(percentages?.ToList(), (a, v) => a.Percents = v);

		public PercentilesBucketAggregationDescriptor Percents(params double[] percentages) =>
			Assign(percentages?.ToList(), (a, v) => a.Percents = v);
	}
}

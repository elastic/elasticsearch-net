using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<PercentilesBucketAggregation>))]
	public interface IPercentilesBucketAggregation : IPipelineAggregation
	{
		[JsonProperty("percents")]
		IEnumerable<double> Percents { get; set; }
	}

	public class PercentilesBucketAggregation
		: PipelineAggregationBase, IPercentilesBucketAggregation
	{
		public IEnumerable<double> Percents { get; set; }

		internal PercentilesBucketAggregation() { }

		public PercentilesBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath)
		{ }

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

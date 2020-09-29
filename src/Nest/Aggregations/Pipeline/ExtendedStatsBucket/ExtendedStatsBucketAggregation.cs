// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

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

		public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma) => Assign(sigma, (a, v) => a.Sigma = v);
	}
}

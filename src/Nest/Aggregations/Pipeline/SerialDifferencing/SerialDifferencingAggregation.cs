using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SerialDifferencingAggregation))]
	public interface ISerialDifferencingAggregation : IPipelineAggregation
	{
		[DataMember(Name ="lag")]
		int? Lag { get; set; }
	}

	public class SerialDifferencingAggregation : PipelineAggregationBase, ISerialDifferencingAggregation
	{
		internal SerialDifferencingAggregation() { }

		public SerialDifferencingAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public int? Lag { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.SerialDifferencing = this;
	}

	public class SerialDifferencingAggregationDescriptor
		: PipelineAggregationDescriptorBase<SerialDifferencingAggregationDescriptor, ISerialDifferencingAggregation, SingleBucketsPath>
			, ISerialDifferencingAggregation
	{
		int? ISerialDifferencingAggregation.Lag { get; set; }

		public SerialDifferencingAggregationDescriptor Lag(int? lag) => Assign(a => a.Lag = lag);
	}
}

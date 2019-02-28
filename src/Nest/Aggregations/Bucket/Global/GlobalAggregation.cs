using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GlobalAggregation))]
	public interface IGlobalAggregation : IBucketAggregation { }

	public class GlobalAggregation : BucketAggregationBase, IGlobalAggregation
	{
		internal GlobalAggregation() { }

		public GlobalAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Global = this;
	}

	public class GlobalAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GlobalAggregationDescriptor<T>, IGlobalAggregation, T>
			, IGlobalAggregation
		where T : class { }
}

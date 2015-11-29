using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<GlobalAggregation>))]
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
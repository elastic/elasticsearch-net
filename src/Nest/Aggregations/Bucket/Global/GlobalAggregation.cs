using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<GlobalAggregation>))]
	public interface IGlobalAggregation : IBucketAggregation { }

	public class GlobalAggregation : BucketAggregationBase, IGlobalAggregation
	{
		public override string TypeName => "global";

		internal GlobalAggregation() { }

		public GlobalAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Global = this;
	}

	public class GlobalAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<GlobalAggregationDescriptor<T>, IGlobalAggregation, T>
			, IGlobalAggregation
		where T : class
	{
		public override string TypeName => "global";
	}
}

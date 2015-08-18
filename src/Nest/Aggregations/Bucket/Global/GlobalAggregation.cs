using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GlobalAggregator>))]
	public interface IGlobalAggregator : IBucketAggregator { }

	public class GlobalAggregator : BucketAggregator, IGlobalAggregator { }

	public class GlobalAgg : BucketAgg, IGlobalAggregator
	{
		public GlobalAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Global = this;
	}

	public class GlobalAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<GlobalAggregatorDescriptor<T>, IGlobalAggregator, T>
			, IGlobalAggregator
		where T : class { }
}
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<GlobalAggregator>))]
	public interface IGlobalAggregator : IBucketAggregator { }

	public class GlobalAggregator : BucketAggregator, IGlobalAggregator { }

	public class GlobalAggregationDescriptor<T> 
		: BucketAggregationBaseDescriptor<GlobalAggregationDescriptor<T>, IGlobalAggregator, T>
			, IGlobalAggregator
		where T : class { }
}
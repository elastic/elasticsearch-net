using Newtonsoft.Json;

namespace Nest
{
	public class GlobalAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GlobalAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("global")] internal readonly object _Global = new object {};
	}
}
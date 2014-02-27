using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public class GlobalAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GlobalAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("global")] internal readonly object _Global = new object {};
	}
}
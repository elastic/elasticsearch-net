using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	public class GlobalAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GlobalAggregationDescriptor<T>, T>
		, ICustomJson
		where T : class
	{
		internal readonly object _Global = new object {};

		object ICustomJson.GetCustomJson()
		{
			return this._Global;
		}
	}
}
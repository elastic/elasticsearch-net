using Newtonsoft.Json;

namespace Nest
{
	public interface IFilter
	{
		[JsonIgnore]
		bool IsConditionless { get; }

		[JsonProperty(PropertyName = "_cache")]
		bool? Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string CacheName { get; set; }

		[JsonProperty(PropertyName = "_cache_key")]
		string CacheKey { get; set; }
	}
}
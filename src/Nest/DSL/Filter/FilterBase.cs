using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class FilterBase : IFilter
	{
		[JsonIgnore]
		bool IFilter.IsConditionless { get { throw new NotImplementedException();} }

		[JsonProperty(PropertyName = "_cache")]
		bool? IFilter.Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string IFilter.CacheName { get; set; }

		[JsonProperty(PropertyName = "_cache_key")]
		string IFilter.CacheKey { get; set; }

		protected IDictionary<object, object> FieldNameAsKeyFormat(object field, object fieldValue, Action<FluentDictionary<object, object>> moreProperties = null)
		{
			var dict = new FluentDictionary<object, object> {
				{ field, fieldValue},
			};
			if (moreProperties != null)
				moreProperties(dict);

			var fb = (IFilter)this;
			if (fb.Cache.HasValue) dict.Add("_cache", fb.Cache);
			if (!fb.CacheName.IsNullOrEmpty()) dict.Add("_name", fb.CacheName);
			if (!fb.CacheKey.IsNullOrEmpty()) dict.Add("_cache_key", fb.CacheKey);
			return dict;
		}

		protected Dictionary<string, object> ReadToDict<TFilter>(JsonReader reader, JsonSerializer serializer, TFilter filter)
			where TFilter : IFilter
		{
			var dict = new Dictionary<string, object>();
			serializer.Populate(reader, dict);
			if (dict.Count == 0) return dict;
			object cache, cacheKey, cacheName;
			if (dict.TryGetValue("_cache", out cache))
				filter.Cache = cache as bool?;

			if (dict.TryGetValue("_cache_key", out cacheKey))
				filter.CacheKey = cacheKey as string;

			if (dict.TryGetValue("_name", out cacheName))
				filter.CacheName = cacheName as string;

			dict.Remove("_cache");
			dict.Remove("_cache_key");
			dict.Remove("_name");
			return dict;

		}
	}
}

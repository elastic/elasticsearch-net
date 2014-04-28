using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IFilterBase
	{
		[JsonIgnore]
		bool IsConditionless { get; }

		[JsonProperty(PropertyName = "_cache")]
		bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string _Name { get; set; }

		[JsonProperty(PropertyName = "_cache_key")]
		string _CacheKey { get; set; }
	}

	public interface IFieldNameFilter : IFilterBase
	{
		string GetFieldName();
	}

	public abstract class FilterBase : IFilterBase
	{

		[JsonIgnore]
		bool IFilterBase.IsConditionless { get { throw new NotImplementedException();} }

		[JsonProperty(PropertyName = "_cache")]
		bool? IFilterBase._Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string IFilterBase._Name { get; set; }

		[JsonProperty(PropertyName = "_cache_key")]
		string IFilterBase._CacheKey { get; set; }

		protected IDictionary<object, object> FieldNameAsKeyFormat(object field, object fieldValue, Action<FluentDictionary<object, object>> moreProperties = null)
		{
			var dict = new FluentDictionary<object, object> {
				{ field, fieldValue},
			};
			if (moreProperties != null)
				moreProperties(dict);

			var fb = (IFilterBase)this;
			if (fb._Cache.HasValue) dict.Add("_cache", fb._Cache);
			if (!fb._Name.IsNullOrEmpty()) dict.Add("_name", fb._Name);
			if (!fb._CacheKey.IsNullOrEmpty()) dict.Add("_cache_key", fb._CacheKey);
			return dict;
		}
 

	}
}

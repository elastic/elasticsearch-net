using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IFilterBase
	{
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

		bool IFilterBase.IsConditionless { get { throw new NotImplementedException();} }

		[JsonProperty(PropertyName = "_cache")]
		bool? IFilterBase._Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		string IFilterBase._Name { get; set; }

		[JsonProperty(PropertyName = "_cache_key")]
		string IFilterBase._CacheKey { get; set; }
	}
}

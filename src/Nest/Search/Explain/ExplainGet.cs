using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ExplainGet<T> where T : class
	{
		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }
		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }

		[JsonProperty(PropertyName = "fields")]
		internal IDictionary<string, object> _fields { get; set; }

	}
}

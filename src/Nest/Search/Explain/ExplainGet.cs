using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class InstantGet<T> where T : class
	{
		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }

		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }

		[JsonProperty(PropertyName = "fields")]
		public FieldValues Fields { get; internal set; }

	}
}

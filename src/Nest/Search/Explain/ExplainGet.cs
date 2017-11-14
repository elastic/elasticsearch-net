using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class InstantGet<T> where T : class
	{
		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[JsonProperty("_source")]
		public T Source { get; internal set; }

		[JsonProperty("fields")]
		public FieldValues Fields { get; internal set; }

	}
}

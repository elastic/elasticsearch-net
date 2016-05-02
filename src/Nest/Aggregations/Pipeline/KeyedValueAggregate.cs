using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public class KeyedValueAggregate : ValueAggregate
	{
		[JsonProperty("keys")]
		public IList<string> Keys { get; internal set; }
	}
}

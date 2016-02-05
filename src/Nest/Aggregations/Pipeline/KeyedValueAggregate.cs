using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public class KeyedValueAggregate : ValueAggregate
	{
		[JsonProperty("keys")]
		public List<string> Keys { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class GlobalStats : Stats
	{

		[JsonProperty(PropertyName = "indices")]
		[Obsolete("since 0.90 this is no longer available as a property of result.Stats use result.Indices directly")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, Stats> Indices { get; set; }
	}
}

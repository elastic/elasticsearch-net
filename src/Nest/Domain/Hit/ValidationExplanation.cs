using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ValidationExplanation
	{
		[JsonProperty(PropertyName = "index")]
		public string Index { get; internal set; }
		[JsonProperty(PropertyName = "description")]
		public bool Valid { get; internal set; }
		[JsonProperty(PropertyName = "error")]
		public string Error { get; internal set; }
		
	}
}

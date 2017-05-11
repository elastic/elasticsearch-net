using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatTemplatesRecord : ICatRecord
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("index_patterns")]
		public string IndexPatterns { get; set; }

		[JsonProperty("order")]
		public long Order { get; set; }

		[JsonProperty("version")]
		public long Version { get; set; }
	}
}

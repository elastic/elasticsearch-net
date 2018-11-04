using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatTemplatesRecord : ICatRecord
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("order")]
		public long Order { get; set; }

		[JsonProperty("template")]
		[Obsolete("will be renamed to IndexPatterns in elasticsearch 6.0")]
		public string Template { get; set; }

		[JsonProperty("version")]
		public long Version { get; set; }
	}
}

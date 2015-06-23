using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonConverter(typeof(TermsIncludeExcludeConverter))]
	public class TermsIncludeExclude
	{
		[JsonProperty("pattern")]
		public string Pattern { get; set; }
		
		[JsonProperty("flags")]
		public string Flags { get; set; }
		
		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }
	}
}
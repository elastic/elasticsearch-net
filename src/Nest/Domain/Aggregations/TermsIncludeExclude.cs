using Newtonsoft.Json;

namespace Nest
{
	public class TermsIncludeExclude
	{
		[JsonProperty("pattern")]
		public string Pattern { get; set; }
		[JsonProperty("flags")]
		public string Flags { get; set; }
	}
}
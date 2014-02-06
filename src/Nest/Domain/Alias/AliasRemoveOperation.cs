using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class AliasRemoveOperation
	{
		[JsonProperty("index")]
		public IndexNameMarker Index { get; set; }
		[JsonProperty("alias")]
		public string Alias { get; set; }
	}
}
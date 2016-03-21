using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatHelpRecord : ICatRecord
	{
		[JsonProperty("endpoint")]
		public string Endpoint { get; set; }

	}
}

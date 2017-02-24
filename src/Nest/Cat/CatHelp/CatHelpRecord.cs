using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class CatHelpRecord : ICatRecord
	{
		[JsonProperty("endpoint")]
		public string Endpoint { get; set; }

	}
}

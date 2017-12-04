using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(BulkErrorJsonConverter))]
	public class BulkError : Error
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		public override string ToString()
		{
			var cause = (CausedBy != null) ?
				$" CausedBy:\n{CausedBy}" :
				string.Empty;

			return $"Type: {Type} Reason: \"{Reason}\"{cause}";
		}
	}
}

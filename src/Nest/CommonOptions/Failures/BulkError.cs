using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ErrorCauseJsonConverter<BulkError>))]
	public class BulkError : Error
	{
		public string Index => Metadata?.Index;

		public int Shard => Metadata.Shard.GetValueOrDefault();

		public override string ToString()
		{
			var cause = (CausedBy != null) ? $" CausedBy:\n{CausedBy}" : string.Empty;

			return $"Type: {Type} Reason: \"{Reason}\"{cause}";
		}
	}
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggestResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		IDictionary<string, Suggest[]> Suggestions { get; set; }
	}

	[JsonObject]
	[JsonConverter(typeof(SuggestResponseJsonConverter))]
	public class SuggestResponse : ResponseBase, ISuggestResponse
	{
		public ShardsMetaData Shards { get; internal set; }

		public IDictionary<string, Suggest[]> Suggestions { get; set;} = new Dictionary<string, Suggest[]>();
	}
}

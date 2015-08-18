using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface ISuggestResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		IDictionary<string, Suggest[]> Suggestions { get; set; }
	}

	[JsonObject]
	[JsonConverter(typeof(SuggestResponseJsonConverter))]
	public class SuggestResponse : BaseResponse, ISuggestResponse
	{
		public ShardsMetaData Shards { get; internal set; }

		public IDictionary<string, Suggest[]> Suggestions { get; set;} = new Dictionary<string, Suggest[]>();
	}
}
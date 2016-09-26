using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggestResponse<T> : IResponse
		where T : class
	{
		ShardsMetaData Shards { get; }
		IDictionary<string, Suggest<T>[]> Suggestions { get; set; }
	}

	[JsonObject]
	[JsonConverter(typeof(SuggestResponseJsonConverter))]
	public class SuggestResponse<T> : ResponseBase, ISuggestResponse<T>
		where T : class
	{
		public SuggestResponse() { }
		internal SuggestResponse(ShardsMetaData metaData, IDictionary<string, object> dict)
		{
			this.Shards = metaData;
			this.Suggestions = dict
				.Select(kv => new { k = kv.Key, v = kv.Value as Suggest<T>[] })
				.Where(kv => kv.v != null)
				.ToDictionary(kv => kv.k, kv => kv.v);
		}

		public ShardsMetaData Shards { get; internal set; }

		public IDictionary<string, Suggest<T>[]> Suggestions { get; set; } = new Dictionary<string, Suggest<T>[]>();
	}
}

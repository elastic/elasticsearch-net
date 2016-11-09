using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggestResponse<T> : IResponse
		where T : class
	{
		ShardsMetaData Shards { get; }
		IReadOnlyDictionary<string, IReadOnlyCollection<Suggest<T>>> Suggestions { get; }
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
			if (dict == null) return;
			this.Suggestions = dict
				.Select(kv => new { k = kv.Key, v = kv.Value as Suggest<T>[] })
				.Where(kv => kv.v != null)
				.ToDictionary(kv => kv.k, kv => (IReadOnlyCollection<Suggest<T>>)kv.v);
		}

		public ShardsMetaData Shards { get; internal set; }

		public IReadOnlyDictionary<string, IReadOnlyCollection<Suggest<T>>> Suggestions { get; set; } = EmptyReadOnly<string, IReadOnlyCollection<Suggest<T>>>.Dictionary;
	}
}

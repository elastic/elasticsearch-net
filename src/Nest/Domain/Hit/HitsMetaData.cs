using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class HitsMetaData<T> where T : class
    {
        [JsonProperty("total")]
        public long Total { get; internal set; }

        [JsonProperty("max_score")]
        public double MaxScore { get; internal set; }

        [JsonProperty("hits")]
        public List<IHit<T>> Hits { get; internal set; }


    }

	public class InnerHitsResult
	{
		
        [JsonProperty("hits")]
		public InnerHitsMetaData Hits { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			return this.Hits == null ? Enumerable.Empty<T>() : this.Hits.Documents<T>();
		}
	}

	public class InnerHitsMetaData
	{
        [JsonProperty("total")]
        public long Total { get; internal set; }

        [JsonProperty("max_score")]
        public double? MaxScore { get; internal set; }

        [JsonProperty("hits")]
        public List<Hit<IDocument>> Hits { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			if (this.Hits == null || !this.Hits.HasAny())
				return Enumerable.Empty<T>();
			return this.Hits.Select(hit => hit.Source.As<T>()).ToList();
		}
	}

}

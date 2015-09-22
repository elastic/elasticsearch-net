using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class InnerHitsMetaData
	{
		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("max_score")]
		public double? MaxScore { get; internal set; }

		[JsonProperty("hits")]
		public List<Hit<ILazyDocument>> Hits { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			if (this.Hits == null || !this.Hits.HasAny())
				return Enumerable.Empty<T>();
			return this.Hits.Select(hit => hit.Source.As<T>()).ToList();
		}
	}
}
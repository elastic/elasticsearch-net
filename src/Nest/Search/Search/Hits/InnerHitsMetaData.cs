using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class InnerHitsMetadata
	{
		[JsonProperty("hits")]
		public List<Hit<ILazyDocument>> Hits { get; internal set; }

		[JsonProperty("max_score")]
		public double? MaxScore { get; internal set; }

		[JsonProperty("total")]
		public HitsTotal Total { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			if (Hits == null || !Hits.HasAny())
				return Enumerable.Empty<T>();

			return Hits.Select(hit => hit.Source.As<T>()).ToList();
		}
	}
}

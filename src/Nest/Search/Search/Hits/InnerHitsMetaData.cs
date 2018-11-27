using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization;

namespace Nest
{
	public class InnerHitsMetadata
	{
		[DataMember(Name ="hits")]
		public List<Hit<ILazyDocument>> Hits { get; internal set; }

		[DataMember(Name ="max_score")]
		public double? MaxScore { get; internal set; }

		[DataMember(Name = "total")]
		public HitsTotal Total { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			if (Hits == null || Hits.Count == 0)
				return Enumerable.Empty<T>();

			return Hits.Select(hit => hit.Source.As<T>()).ToList();
		}
	}
}

using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	public class ShardProfile
	{
		[DataMember(Name ="aggregations")]
		public IReadOnlyCollection<AggregationProfile> Aggregations { get; internal set; } =
			EmptyReadOnly<AggregationProfile>.Collection;

		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		[DataMember(Name ="searches")]
		public IReadOnlyCollection<SearchProfile> Searches { get; internal set; } =
			EmptyReadOnly<SearchProfile>.Collection;
	}
}

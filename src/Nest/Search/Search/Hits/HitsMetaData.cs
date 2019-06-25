using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HitsMetadata<>))]
	public interface IHitsMetadata<out T> where T : class
	{
		[DataMember(Name = "hits")]
		IReadOnlyCollection<IHit<T>> Hits { get; }

		[DataMember(Name = "max_score")]
		double? MaxScore { get; }

		[DataMember(Name = "total")]
		TotalHits Total { get; }
	}


	public class HitsMetadata<T> : IHitsMetadata<T>
		where T : class
	{
		[DataMember(Name = "hits")]
		public IReadOnlyCollection<IHit<T>> Hits { get; internal set; } = EmptyReadOnly<IHit<T>>.Collection;

		[DataMember(Name = "max_score")]
		public double? MaxScore { get; internal set; }

		[DataMember(Name = "total")]
		public TotalHits Total { get; internal set; }
	}
}

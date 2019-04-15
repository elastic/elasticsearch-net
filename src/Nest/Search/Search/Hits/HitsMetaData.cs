using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
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


	[DataContract]
	public class HitsMetadata<T> where T : class
	{
		[DataMember(Name = "hits")]
		public IReadOnlyCollection<IHit<T>> Hits { get; internal set; } = EmptyReadOnly<IHit<T>>.Collection;

		[DataMember(Name = "max_score")]
		public double? MaxScore { get; internal set; }

		[DataMember(Name = "total")]
		public TotalHits Total { get; internal set; }
	}
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class HitsMetadata<T> where T : class
	{
		[DataMember(Name ="hits")]
		public IReadOnlyCollection<IHit<T>> Hits { get; internal set; } = EmptyReadOnly<IHit<T>>.Collection;

		[DataMember(Name ="max_score")]
		public double MaxScore { get; internal set; }

		[DataMember(Name ="total")]
		public HitsTotal Total { get; internal set; }
	}
}

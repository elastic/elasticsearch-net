using System.Collections.Generic;

namespace Nest
{
	public interface IBucket { }

	public abstract class BucketBase : AggregateDictionary, IBucket
	{
		protected BucketBase(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }
	}
}

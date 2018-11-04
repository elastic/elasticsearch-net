using System.Collections.Generic;

namespace Nest
{
	public interface IBucket { }

	public abstract class BucketBase : AggregationsHelper, IBucket
	{
		protected BucketBase() { }

		protected BucketBase(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }
	}
}

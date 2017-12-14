using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public interface IBucket { }

	public abstract class BucketBase : AggregateDictionary, IBucket
	{
		protected BucketBase(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }
	}
}

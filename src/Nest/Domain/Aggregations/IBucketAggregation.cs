using System.Collections.Generic;

namespace Nest
{
	public interface IBucketAggregation : IAggregation
	{
		IDictionary<string, IAggregation> Aggregations { get; }
	}
}
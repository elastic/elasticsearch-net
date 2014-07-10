using System.Collections.Generic;
using Nest;

namespace Nest
{
	public interface IBucketAggregation : IAggregation
	{
		IDictionary<string, IAggregation> Aggregations { get; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	[ExactContractJsonConverter(typeof(AggregationResultJsonConverter))]
	public interface IAggregationItem
	{
	}

	public interface IAggregationResult : IAggregationItem
	{
		IDictionary<string, object> Meta { get; set; }
	}
}

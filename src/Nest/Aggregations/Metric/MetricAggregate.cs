using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class MetricAggregateBase : IAggregate
	{
		public IDictionary<string, object> Meta { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class MetricBase : IAggregationResult
	{
		public IDictionary<string, object> Meta { get; set; }
	}
}

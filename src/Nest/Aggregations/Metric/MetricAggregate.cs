using System;
using System.Collections.Generic;

namespace Nest_5_2_0
{
	public abstract class MetricAggregateBase : IAggregate
	{
		public IReadOnlyDictionary<string, object> Meta { get;  set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

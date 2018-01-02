using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class MetricAggregateBase : IAggregate
	{
		[JsonProperty("meta")]
		public IReadOnlyDictionary<string, object> Meta { get;  set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

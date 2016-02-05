using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public class PercentilesAggregate : MetricAggregateBase
	{
		[JsonProperty("values")]
		public Dictionary<string, double> Values { get; internal set; }
	}
}
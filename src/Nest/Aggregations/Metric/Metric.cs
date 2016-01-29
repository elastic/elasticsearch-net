using System;
using System.Collections.Generic;

namespace Nest
{
	public interface IMetric : IAggregationResult
	{
	}

	public abstract class MetricBase : IMetric
	{
		public Dictionary<string, object> Meta { get; internal set; }
	}
}

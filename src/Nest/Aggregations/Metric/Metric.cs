using System;
using System.Collections.Generic;

namespace Nest
{
	public interface IMetric : IAggregationResult
	{
	}

	public abstract class MetricBase : IMetric
	{
		public IDictionary<string, object> Meta { get; set; }
	}
}

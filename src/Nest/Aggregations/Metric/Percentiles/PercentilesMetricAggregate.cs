using System.Collections.Generic;

namespace Nest
{

	public class PercentileItem
	{
		public double Percentile { get; internal set; }
		public double Value { get; internal set; }
	}

	public class PercentilesAggregate : MetricAggregateBase
	{
		public IList<PercentileItem> Items { get; internal set; } = new List<PercentileItem>();
	}
}
// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Nest
{
	public class PercentileItem
	{
		public double Percentile { get; internal set; }
		public double? Value { get; internal set; }
	}

	public class PercentilesAggregate : MetricAggregateBase
	{
		public IList<PercentileItem> Items { get; internal set; } = new List<PercentileItem>();
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public class StatsAggregate : MetricAggregateBase
	{
		public double? Average { get; set; }
		public long Count { get; set; }
		public double? Max { get; set; }
		public double? Min { get; set; }
		public double Sum { get; set; }
	}
}

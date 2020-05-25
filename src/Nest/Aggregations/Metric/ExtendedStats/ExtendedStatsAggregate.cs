// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public class ExtendedStatsAggregate : StatsAggregate
	{
		public double? StdDeviation { get; set; }
		public StandardDeviationBounds StdDeviationBounds { get; set; }
		public double? SumOfSquares { get; set; }
		public double? Variance { get; set; }
	}

	public class StandardDeviationBounds
	{
		public double? Lower { get; set; }
		public double? Upper { get; set; }
	}
}

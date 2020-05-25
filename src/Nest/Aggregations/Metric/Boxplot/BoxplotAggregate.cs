// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class BoxplotAggregate : MetricAggregateBase
	{
		public double Min { get; set; }

		public double Max { get; set; }

		public double Q1 { get; set; }

		public double Q2 { get; set; }

		public double Q3 { get; set; }
	}
}

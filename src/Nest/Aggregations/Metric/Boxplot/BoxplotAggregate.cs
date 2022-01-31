// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class BoxplotAggregate : MetricAggregateBase
	{
		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Min { get; set; }

		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Max { get; set; }

		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Q1 { get; set; }

		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Q2 { get; set; }

		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Q3 { get; set; }

		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Lower { get; set; }

		[JsonFormatter(typeof(StringDoubleFormatter))]
		public double Upper { get; set; }
	}
}

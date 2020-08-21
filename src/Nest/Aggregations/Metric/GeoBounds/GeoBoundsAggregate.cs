// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class GeoBoundsAggregate : MetricAggregateBase
	{
		public GeoBoundsAggregate() => Bounds = new GeoBounds();

		public GeoBounds Bounds { get; set; }
	}

	public class GeoBounds
	{
		public LatLon BottomRight { get; set; }
		public LatLon TopLeft { get; set; }
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class GeoLineAggregate : MetricAggregateBase
	{
		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "geometry")]
		public LineStringGeoShape Geometry { get; set; }

		[DataMember(Name = "properties")]
		public GeoLineProperties Properties { get; set; }
	}

	[DataContract]
	public class GeoLineProperties
	{
		[DataMember(Name = "complete")]
		public bool Complete { get; set; }

		[DataMember(Name = "sort_values")]
		public IEnumerable<double> SortValues { get; set; }
	}
}

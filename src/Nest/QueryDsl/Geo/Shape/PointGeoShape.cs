// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IPointGeoShape>))]
	public interface IPointGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		GeoCoordinate Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<PointGeoShape>))]
	public class PointGeoShape : GeoShapeBase, IPointGeoShape
	{
		internal PointGeoShape() : base("point") { }

		public PointGeoShape(GeoCoordinate coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public GeoCoordinate Coordinates { get; set; }
	}
}

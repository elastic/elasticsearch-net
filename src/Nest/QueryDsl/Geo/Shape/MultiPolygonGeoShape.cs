using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IMultiPolygonGeoShape>))]
	public interface IMultiPolygonGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<MultiPolygonGeoShape>))]
	public class MultiPolygonGeoShape : GeoShapeBase, IMultiPolygonGeoShape
	{
		internal MultiPolygonGeoShape() : base("multipolygon") { }

		public MultiPolygonGeoShape(IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> Coordinates { get; set; }
	}
}

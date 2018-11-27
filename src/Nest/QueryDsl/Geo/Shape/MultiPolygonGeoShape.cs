using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IMultiPolygonGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> Coordinates { get; set; }
	}

	public class MultiPolygonGeoShape : GeoShapeBase, IMultiPolygonGeoShape
	{
		internal MultiPolygonGeoShape() : base("multipolygon") { }

		public MultiPolygonGeoShape(IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> Coordinates { get; set; }
	}
}

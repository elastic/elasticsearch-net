using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ILineStringGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class LineStringGeoShape : GeoShapeBase, ILineStringGeoShape
	{
		internal LineStringGeoShape() : base("linestring") { }

		public LineStringGeoShape(IEnumerable<GeoCoordinate> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}

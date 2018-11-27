using System;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IPointGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		GeoCoordinate Coordinates { get; set; }
	}

	public class PointGeoShape : GeoShapeBase, IPointGeoShape
	{
		internal PointGeoShape() : base("point") { }

		public PointGeoShape(GeoCoordinate coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public GeoCoordinate Coordinates { get; set; }
	}
}

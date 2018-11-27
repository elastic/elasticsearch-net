using System;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ICircleGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		GeoCoordinate Coordinates { get; set; }

		[DataMember(Name ="radius")]
		string Radius { get; set; }
	}

	public class CircleGeoShape : GeoShapeBase, ICircleGeoShape
	{
		internal CircleGeoShape() : base("circle") { }

		public CircleGeoShape(GeoCoordinate coordinates, string radius) : this()
		{
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
			if (radius == null) throw new ArgumentNullException(nameof(radius));
			if (radius.Length == 0) throw new ArgumentException("cannot be empty", nameof(radius));

			Radius = radius;
		}

		public GeoCoordinate Coordinates { get; set; }

		public string Radius { get; set; }
	}
}

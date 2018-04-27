using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICircleGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		GeoCoordinate Coordinates { get; set; }

		[JsonProperty("radius")]
		string Radius { get; set; }
	}

	public class CircleGeoShape : GeoShapeBase, ICircleGeoShape
	{
		internal CircleGeoShape() : base("circle") { }

		public CircleGeoShape(GeoCoordinate coordinates, string radius) : this()
		{
			this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
			if (radius == null) throw new ArgumentNullException(nameof(radius));
			if (radius.Length == 0) throw new ArgumentException("cannot be empty", nameof(radius));
			this.Radius = radius;
		}

		public GeoCoordinate Coordinates { get; set; }

		public string Radius { get; set; }
	}
}

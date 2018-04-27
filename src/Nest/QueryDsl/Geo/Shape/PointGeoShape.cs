using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPointGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		GeoCoordinate Coordinates { get; set; }
	}

	public class PointGeoShape : GeoShapeBase, IPointGeoShape
	{
		internal PointGeoShape() : base("point") { }

		public PointGeoShape(GeoCoordinate coordinates) : this() =>
			this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public GeoCoordinate Coordinates { get; set; }
	}
}

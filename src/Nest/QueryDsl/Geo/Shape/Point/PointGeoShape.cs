using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IPointGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		GeoCoordinate Coordinates { get; set; }
	}

	public class PointGeoShape : GeoShapeBase, IPointGeoShape
	{
		public PointGeoShape() : this(null) { }

		public PointGeoShape(GeoCoordinate coordinates) 
			: base("point") 
		{
			this.Coordinates = coordinates;
		}

		public GeoCoordinate Coordinates { get; set; }
	}
}

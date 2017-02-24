using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface ILineStringGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class LineStringGeoShape : GeoShapeBase, ILineStringGeoShape
	{
		public LineStringGeoShape() : this(null) { }

		public LineStringGeoShape(IEnumerable<GeoCoordinate> coordinates) 
			: base("linestring") 
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}

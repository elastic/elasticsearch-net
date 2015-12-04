using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IPointGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		GeoCoordinate Coordinates { get; set; }
	}

	public class PointGeoShape : GeoShape, IPointGeoShape
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

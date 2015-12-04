using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ICircleGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		GeoCoordinate Coordinates { get; set; }

		[JsonProperty("radius")]
		string Radius { get; set; }
	}

	public class CircleGeoShape : GeoShape, ICircleGeoShape
	{
		public CircleGeoShape() : this(null) { }

		public CircleGeoShape(GeoCoordinate location) : base("circle")
		{
			this.Coordinates = location;
		}

		public GeoCoordinate Coordinates { get; set; }

		public string Radius { get; set; }
	}
}

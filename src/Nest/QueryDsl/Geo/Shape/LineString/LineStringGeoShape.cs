using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ILineStringGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class LineStringGeoShape : GeoShape, ILineStringGeoShape
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

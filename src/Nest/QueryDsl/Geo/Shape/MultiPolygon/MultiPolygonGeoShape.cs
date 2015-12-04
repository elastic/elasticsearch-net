using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IMultiPolygonGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> Coordinates { get; set; }
	}

	public class MultiPolygonGeoShape : GeoShape, IMultiPolygonGeoShape
	{
		public MultiPolygonGeoShape() : this(null) { }

		public MultiPolygonGeoShape(IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> coordinates) 
			: base("multipolygon") 
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> Coordinates { get; set; }
	}
}

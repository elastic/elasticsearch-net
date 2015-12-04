using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IPolygonGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	public class PolygonGeoShape : GeoShape, IPolygonGeoShape
	{
		public PolygonGeoShape() : this(null) { }

		public PolygonGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) 
			: base("polygon") 
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}

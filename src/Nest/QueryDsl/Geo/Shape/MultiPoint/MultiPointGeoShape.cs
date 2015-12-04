using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IMultiPointGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}

	public class MultiPointGeoShape : GeoShape, IMultiPointGeoShape
	{
		public MultiPointGeoShape() : this(null) { }

		public MultiPointGeoShape(IEnumerable<GeoCoordinate> coordinates) 
			: base("multipoint")
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<GeoCoordinate> Coordinates { get; set; }
	}
}

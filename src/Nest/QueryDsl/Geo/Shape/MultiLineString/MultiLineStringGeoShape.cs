using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IMultiLineStringGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	public class MultiLineStringGeoShape : GeoShape, IMultiLineStringGeoShape
	{
		public MultiLineStringGeoShape() : this(null) { }

		public MultiLineStringGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) 
			: base("multilinestring") 
		{
			this.Coordinates = coordinates;
		}

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}

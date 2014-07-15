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
		IEnumerable<IEnumerable<double>> Coordinates { get; set; }
	}

	public class LineStringGeoShape : GeoShape, ILineStringGeoShape
	{
		public LineStringGeoShape() : this(null) { }

		public LineStringGeoShape(IEnumerable<IEnumerable<double>> coordinates) 
			: base("linestring") 
		{
			this.Coordinates = coordinates ?? new List<List<double>>();
		}

		public IEnumerable<IEnumerable<double>> Coordinates { get; set; }
	}
}

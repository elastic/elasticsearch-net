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
		IEnumerable<double> Coordinates { get; set; }
	}

	public class PointGeoShape : GeoShape, IPointGeoShape
	{
		public PointGeoShape() : this(null) { }

		public PointGeoShape(IEnumerable<double> coordinates) 
			: base("point") 
		{
			this.Coordinates = coordinates ?? new List<double>();
		}

		public IEnumerable<double> Coordinates { get; set; }
	}
}

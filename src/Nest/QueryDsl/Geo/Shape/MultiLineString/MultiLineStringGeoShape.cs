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
		IEnumerable<IEnumerable<IEnumerable<double>>> Coordinates { get; set; }
	}

	public class MultiLineStringGeoShape : GeoShape, IMultiLineStringGeoShape
	{
		public MultiLineStringGeoShape() : this(null) { }

		public MultiLineStringGeoShape(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates) 
			: base("multilinestring") 
		{
			this.Coordinates = coordinates ?? new List<List<List<double>>>();
		}

		public IEnumerable<IEnumerable<IEnumerable<double>>> Coordinates { get; set; }
	}
}

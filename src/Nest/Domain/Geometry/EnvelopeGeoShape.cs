using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IEnvelopeGeoShape : IGeoShape
	{
		[JsonProperty("coordinates")]
		IEnumerable<IEnumerable<double>> Coordinates { get; set; }
	}

	public class EnvelopeGeoShape : GeoShape, IEnvelopeGeoShape
	{
		public EnvelopeGeoShape() : this(null) { }

		public EnvelopeGeoShape(IEnumerable<IEnumerable<double>> coordinates) 
			: base("envelope") 
		{
			this.Coordinates = coordinates ?? new List<List<double>>();
		}

		public IEnumerable<IEnumerable<double>> Coordinates { get; set; }
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(GeoPrecisionConverter))]
	public class GeoPrecision
	{
		public double Precision { get; private set; }
		public GeoPrecisionUnit Unit { get; private set; }

		public GeoPrecision(double precision, GeoPrecisionUnit unit)
		{
			this.Precision = precision;
			this.Unit = unit;
		}
	}
}

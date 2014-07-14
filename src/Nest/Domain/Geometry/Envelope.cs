using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class Envelope 
		: GeometryObject<IEnumerable<IEnumerable<double>>>
	{
		public Envelope() : this(null) { }

		public Envelope(IEnumerable<IEnumerable<double>> coordinates) 
			: base("envelope") 
		{
			this.Coordinates = coordinates ?? new List<List<double>>();
		}
	}
}

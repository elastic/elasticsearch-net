using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class MultiPolygon 
		: GeometryObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>
	{
		public MultiPolygon() : this(null) { }

		public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> coordinates) 
			: base("multipolygon") 
		{
			this.Coordinates = coordinates ?? new List<List<List<List<double>>>>();
		}
	}
}

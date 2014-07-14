using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class LineString 
		: GeometryObject<IEnumerable<IEnumerable<double>>>
	{
		public LineString() : this(null) { }

		public LineString(IEnumerable<IEnumerable<double>> coordinates) 
			: base("linestring") 
		{
			this.Coordinates = coordinates ?? new List<List<double>>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class Point 
		: GeometryObject<IEnumerable<double>>
	{
		public Point() : this(null) { }

		public Point(IEnumerable<double> coordinates) 
			: base("point") 
		{
			this.Coordinates = coordinates ?? new List<double>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class MultiLineString
		: GeometryObject<IEnumerable<IEnumerable<IEnumerable<double>>>>
	{
		public MultiLineString() : this(null) { }

		public MultiLineString(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates) 
			: base("multilinestring") 
		{
			this.Coordinates = coordinates ?? new List<List<List<double>>>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class Polygon :
		GeometryObject< IEnumerable<IEnumerable<IEnumerable<double>>>>
	{
		public Polygon() : this(null) { }

		public Polygon(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates) 
			: base("polygon") 
		{
			this.Coordinates = coordinates ?? new List<List<List<double>>>();
		}
	}
}

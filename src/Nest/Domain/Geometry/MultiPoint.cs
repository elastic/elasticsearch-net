using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class MultiPoint :
		GeometryObject<IEnumerable<IEnumerable<double>>>
	{
		public MultiPoint() : this(null) { }

		public MultiPoint(IEnumerable<IEnumerable<double>> coordinates) 
			: base("multipoint")
		{
			this.Coordinates = coordinates ?? new List<List<double>>();
		}
	}
}

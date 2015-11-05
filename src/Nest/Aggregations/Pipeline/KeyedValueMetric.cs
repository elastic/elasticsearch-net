using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class KeyedValueMetric : ValueMetric
	{
		public IList<string> Keys { get; set; }
	}
}

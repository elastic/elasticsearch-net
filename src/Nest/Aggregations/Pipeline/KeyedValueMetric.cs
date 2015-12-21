using System.Collections.Generic;

namespace Nest
{
	public class KeyedValueMetric : ValueMetric
	{
		public IList<string> Keys { get; set; }
	}
}

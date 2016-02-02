using System.Collections.Generic;

namespace Nest
{
	public class KeyedValueAggregate : ValueAggregate
	{
		public IList<string> Keys { get; set; }
	}
}

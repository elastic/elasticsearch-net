using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class RawOrQueryDescriptor<T> where T : class
	{
		public string Raw { get; set; }
		public BaseQuery Descriptor { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class RawOrFilterDescriptor<T> where T : class
	{
		public string Raw { get; set; }
		public FilterDescriptor<T> Descriptor { get; set; }
	}
}

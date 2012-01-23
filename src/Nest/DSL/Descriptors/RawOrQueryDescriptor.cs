using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL
{
	public class RawOrQueryDescriptor<T> where T : class
	{
		public string RawQuery { get; set; }
		public QueryDescriptor<T> Descriptor { get; set; }
	}
}

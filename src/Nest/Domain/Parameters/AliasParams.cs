using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class AliasParams
	{
		public string Index { get; set; }
		public string Alias { get; set; }
		public string Filter { get; set; }
		public string Routing { get; set; }
		public string SearchRouting { get; set; }
		public string IndexRouting { get; set; }
	}
}

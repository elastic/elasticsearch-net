using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class IdsFilter : FilterBase
	{
		public IEnumerable<string> Type { get; set; }
		public IEnumerable<string> Values { get; set; }
	}
}

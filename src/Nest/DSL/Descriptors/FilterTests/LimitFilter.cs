using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class LimitFilter : FilterBase
	{
		public int Value { get; set;}
	}
}

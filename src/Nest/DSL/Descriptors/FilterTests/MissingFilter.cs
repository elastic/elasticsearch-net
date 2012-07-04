using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class MissingFilter : FilterBase
	{
		public string Field { get; set;}
	}
}

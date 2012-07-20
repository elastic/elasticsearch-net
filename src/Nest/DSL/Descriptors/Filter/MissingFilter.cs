using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class MissingFilter : FilterBase
	{
		[JsonProperty(PropertyName = "field")]
		public string Field { get; set;}
	}
}

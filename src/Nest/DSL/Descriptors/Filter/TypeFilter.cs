using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class TypeFilter : FilterBase
	{
		[JsonProperty(PropertyName = "value")]
		public string Value { get; set;}
	}
}

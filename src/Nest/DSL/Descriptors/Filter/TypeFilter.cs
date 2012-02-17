using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class TypeFilter
	{
		public string Value { get; set;}

		[JsonProperty(PropertyName = "_cache")]
		internal bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }
	}
}

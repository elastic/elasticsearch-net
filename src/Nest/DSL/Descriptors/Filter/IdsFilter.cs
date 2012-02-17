using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class IdsFilter
	{
		public IEnumerable<string> Type { get; set; }
		public IEnumerable<string> Values { get; set; }
		[JsonProperty(PropertyName = "_cache")]
		internal bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }
	}
}

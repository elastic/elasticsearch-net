using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TermsIncludeExcludeJsonConverter))]
	public class TermsIncludeExclude
	{
		[JsonIgnore]
		public string Pattern { get; set; }

		[JsonIgnore]
		[Obsolete("Deprecated in 2.0. Flags can no longer be specified")]
		public string Flags { get; set; }
		
		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }
	}
}

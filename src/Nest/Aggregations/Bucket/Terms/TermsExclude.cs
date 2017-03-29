using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TermsExcludeJsonConverter))]
	public class TermsExclude
	{
		[JsonIgnore]
		public string Pattern { get; set; }

		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }

		public TermsExclude(string pattern)
		{
			Pattern = pattern;
		}

		public TermsExclude(IEnumerable<string> values)
		{
			Values = values;
		}
	}
}
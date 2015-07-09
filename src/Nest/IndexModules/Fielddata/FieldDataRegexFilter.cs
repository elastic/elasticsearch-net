using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataRegexFilter
	{
		[JsonProperty("pattern")]
		public string Pattern { get; set; }
	}

	public class FieldDataRegexQueryDescriptor
	{
		internal FieldDataRegexFilter RegexFilter { get; private set; }

		public FieldDataRegexQueryDescriptor()
		{
			this.RegexFilter = new FieldDataRegexFilter();
		}

		public FieldDataRegexQueryDescriptor Pattern(string pattern)
		{
			this.RegexFilter.Pattern = pattern;
			return this;
		}
	}
}

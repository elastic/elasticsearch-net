
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class FieldDataRegexFilterDescriptor
	{
		internal FieldDataRegexFilter RegexFilter { get; private set; }

		public FieldDataRegexFilterDescriptor()
		{
			this.RegexFilter = new FieldDataRegexFilter();
		}

		public FieldDataRegexFilterDescriptor Pattern(string pattern)
		{
			this.RegexFilter.Pattern = pattern;
			return this;
		}
	}
}

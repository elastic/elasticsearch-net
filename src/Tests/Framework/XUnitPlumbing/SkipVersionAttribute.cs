using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Framework.Versions;

namespace Tests.Framework
{
	public class SkipVersionAttribute : Attribute
	{
		public IList<VersionRange> Ranges { get; }

		public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
		{
			this.Ranges = skipVersionRangesSeparatedByComma.Split(',')
				.Select(r => new VersionRange(r))
				.ToList();
		}
	}
}

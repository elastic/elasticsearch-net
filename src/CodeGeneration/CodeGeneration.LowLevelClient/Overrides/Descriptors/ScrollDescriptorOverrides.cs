using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class ScrollDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams => new []
		{
			"scroll_id", "scroll" 
		};

		public IDictionary<string, string> RenameQueryStringParams => null;
	}
}

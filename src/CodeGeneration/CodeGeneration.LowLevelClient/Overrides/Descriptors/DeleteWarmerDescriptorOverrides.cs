using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class DeleteWarmerDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams => new []
		{
			"name" 
		};

		public IDictionary<string, string> RenameQueryStringParams => null;
	}
}

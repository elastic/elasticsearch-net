using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class PutIndexTemplateDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams => new []
		{
			"order" 
		};

		public IDictionary<string, string> RenameQueryStringParams => null;
	}
}

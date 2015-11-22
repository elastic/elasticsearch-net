using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class UpdateDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams => new []
		{
			"fields" 
		};

		public IDictionary<string, string> RenameQueryStringParams => null;
	}
	
}
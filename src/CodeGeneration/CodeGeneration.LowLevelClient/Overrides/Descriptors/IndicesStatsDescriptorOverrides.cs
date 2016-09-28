using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class IndicesStatsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"types" 
				};
			}
		}
	}
}
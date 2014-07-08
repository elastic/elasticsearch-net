using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class IndicesStatsDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"types" 
				};
			}
		}
		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
}
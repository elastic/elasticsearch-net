using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class UpdateDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"fields" 
				};
			}
		}
		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
}
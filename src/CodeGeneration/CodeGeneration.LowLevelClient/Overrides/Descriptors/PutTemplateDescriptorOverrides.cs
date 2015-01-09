using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class PutTemplateDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"order" 
				};
			}
		}
		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
}

using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class DeleteWarmerDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"name" 
				};
			}
		}

		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
}

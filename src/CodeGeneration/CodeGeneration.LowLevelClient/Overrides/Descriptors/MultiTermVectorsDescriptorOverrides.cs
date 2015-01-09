using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	//CodeGeneration.LowLevelClient.Overrides.Descriptors.MultiTermVectorsDescriptorOverrides
	public class MultiTermVectorsDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"ids"
				};
			}
		}
		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
}

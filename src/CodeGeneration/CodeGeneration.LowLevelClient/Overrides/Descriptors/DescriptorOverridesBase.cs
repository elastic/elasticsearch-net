using System.Collections.Generic;
using CodeGeneration.LowLevelClient.Domain;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public abstract class DescriptorOverridesBase: IDescriptorOverrides
	{
		public virtual IEnumerable<string> SkipQueryStringParams { get; } = null;

		public virtual IDictionary<string, string> RenameQueryStringParams { get; } = null;

		public virtual CsharpMethod PatchMethod(CsharpMethod method)
		{
			return method;
		}
	}
}

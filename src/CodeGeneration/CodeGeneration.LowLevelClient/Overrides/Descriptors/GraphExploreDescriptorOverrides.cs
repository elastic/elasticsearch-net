using System.Linq;
using CodeGeneration.LowLevelClient.Domain;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class GraphExploreDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			var part = method.Parts.First(p => p.Name == "index");
			part.Required = true;
			return method;
		}
	}
}

using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class IndexDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			var part = method.Url.Params.First(p => p.Key == "refresh");
			part.Value.Description = "Refresh the affected shards after performing the operation";
			return method;
		}
	}
}

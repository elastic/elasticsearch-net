using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GraphExploreOverrides : EndpointOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			var part = method.Parts.First(p => p.Name == "index");
			part.Required = true;
			return method;
		}
	}
}

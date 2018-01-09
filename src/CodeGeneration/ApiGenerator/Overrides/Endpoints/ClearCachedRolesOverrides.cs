using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Endpoints
{
	public class ClearCachedRolesOverrides : EndpointOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			var part = method.Parts.First(p => p.Name == "name");
			part.ClrTypeNameOverride = "Names";
			return method;
		}
	}
}

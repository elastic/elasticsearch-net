using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Endpoints
{
	public class ClearCachedRealmsOverrides : EndpointOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			var part = method.Parts.First(p => p.Name == "realms");
			part.ClrTypeNameOverride = "Names";
			return method;
		}
	}
}

using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class ReindexRethrottleDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			var requestParam = method.Url.Params.First(p => p.Key == "requests_per_second");

			//Handle float.PositiveInfinity as though "unlimited"
			requestParam.Value.Generator = (fieldType, mm, original, setter) =>
				$"public {fieldType} {mm} {{ " +
				$"get {{ var q = Q<string>(\"{original}\"); return (q == \"unlimited\")? float.PositiveInfinity : float.Parse(q); }} " +
				$"set {{ Q(\"{original}\", value == float.PositiveInfinity ? \"unlimited\" : value.ToString()); }} " +
				$"}}";

			requestParam.Value.FluentGenerator = (queryStringParamName, mm, original, setter) =>
				$"public {queryStringParamName} {mm.ToPascalCase()}({requestParam.Value.CsharpType(mm)} {mm}) => " +
				$"this.AddQueryString(\"{original}\", {setter} == float.PositiveInfinity ? \"unlimited\" : {setter}.ToString());";

			return base.PatchMethod(method);
		}
	}
}

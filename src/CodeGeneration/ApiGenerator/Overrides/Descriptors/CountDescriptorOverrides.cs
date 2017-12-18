using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class CountDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			method.Url.Params["routing"].Type = "string";
			return method;
		}
	}
}

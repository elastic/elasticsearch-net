using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Endpoints
{
	public class PostJobDataOverrides : EndpointOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			method.Url.Params["reset_start"].Type = "date";
			method.Url.Params["reset_end"].Type = "date";
			return method;
		}
	}
}

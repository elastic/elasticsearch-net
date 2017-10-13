using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class PostJobDataDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			method.Url.Params["reset_start"].Type = "date";
			method.Url.Params["reset_end"].Type = "date";
			return method;
		}
	}
}

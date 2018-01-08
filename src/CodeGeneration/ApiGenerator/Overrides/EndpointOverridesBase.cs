using System.Collections.Generic;
using ApiGenerator.Domain;
using ApiGenerator.Overrides.Descriptors;

namespace ApiGenerator.Overrides
{
	public abstract class EndpointOverridesBase: IEndpointOverrides
	{
		public virtual IEnumerable<string> SkipQueryStringParams { get; } = null;

		public virtual IEnumerable<string> RenderPartial { get; } = null;

		public virtual IDictionary<string, string> RenameQueryStringParams { get; } = null;

		public virtual CsharpMethod PatchMethod(CsharpMethod method)
		{
			return method;
		}
	}
}

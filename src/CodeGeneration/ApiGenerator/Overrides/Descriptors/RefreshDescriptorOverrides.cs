using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class RefreshDescriptorOverrides : DescriptorOverridesBase
	{
		public override CsharpMethod PatchMethod(CsharpMethod method)
		{
			method.Url.Params.Add("force", new NoopApiQueryParameters() { Type = "boolean" });
			method.Url.Params.Add("operation_threading", new NoopApiQueryParameters());
			return method;
		}

		private class NoopApiQueryParameters : ApiQueryParameters
		{
			public NoopApiQueryParameters()
			{
				this.Obsolete = "calling this is a noop";
				this.FluentGenerator = (queryStringParamName, mm, original, setter) => $"public {queryStringParamName} {mm.ToPascalCase()}({CsharpType(mm)} {mm}) => this;";
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGeneration.LowLevelClient.Domain;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class ClearCacheDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"fielddata"
				};
			}
		}

	    public override CsharpMethod PatchMethod(CsharpMethod method)
	    {
	        var part = method.Url.Params.First(p => p.Key == "filter_keys");

            method.Url.Params["filter_keys"] = new FilterKeysApiQueryParameters(part.Value);

	        return base.PatchMethod(method);
	    }

	    private class FilterKeysApiQueryParameters : ApiQueryParameters
	    {
	        public FilterKeysApiQueryParameters(ApiQueryParameters parameters)
	        {
	            this.DeprecatedInFavorOf = parameters.DeprecatedInFavorOf;
	            this.Description = parameters.Description;
	            this.Options = parameters.Options;
	            this.OriginalQueryStringParamName = parameters.OriginalQueryStringParamName;
	            this.Type = parameters.Type;
	        }

	        public override string CsharpPropertyName(string cased)
	        {
	            return "FilterCacheKeys";
	        }
	    }
	}
}

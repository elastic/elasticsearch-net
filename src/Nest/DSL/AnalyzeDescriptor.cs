using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor : 
		IndicesOptionalPathDescriptor<AnalyzeDescriptor, AnalyzeQueryString>
		, IPathInfo<AnalyzeQueryString>
	{

		//TODO add Field() with expression support

		ElasticSearchPathInfo<AnalyzeQueryString> IPathInfo<AnalyzeQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<AnalyzeQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor : 
		IndicesOptionalPathDescriptor<AnalyzeDescriptor, AnalyzeRequestParameters>
		, IPathInfo<AnalyzeRequestParameters>
	{

		ElasticsearchPathInfo<AnalyzeRequestParameters> IPathInfo<AnalyzeRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<AnalyzeRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}

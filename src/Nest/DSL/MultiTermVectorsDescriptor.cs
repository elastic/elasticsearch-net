using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor<T> : IndexTypePathTypedDescriptor<MultiTermVectorsDescriptor<T>, MultiTermVectorsRequestParameters, T>
		, IPathInfo<MultiTermVectorsRequestParameters> where T : class
	{
		ElasticsearchPathInfo<MultiTermVectorsRequestParameters> IPathInfo<MultiTermVectorsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}
}

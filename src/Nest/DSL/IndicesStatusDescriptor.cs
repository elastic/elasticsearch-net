using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("IndicesStatus")]
	public partial class IndicesStatusDescriptor : IndicesOptionalPathDescriptor<IndicesStatusDescriptor, IndicesStatusRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}

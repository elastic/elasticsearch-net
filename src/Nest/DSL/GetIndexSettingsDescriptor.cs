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
	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor : IndexPathDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
}

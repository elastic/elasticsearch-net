using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor : IndexPathDescriptorBase<IndexExistsDescriptor, IndexExistsRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
}
